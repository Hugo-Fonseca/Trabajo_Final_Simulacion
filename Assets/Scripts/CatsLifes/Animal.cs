using UnityEngine;

public class Animal : MonoBehaviour
{
    public AnimalState currentState = AnimalState.Quieto;

    [Header("Movimiento")]
    public float moveSpeed = 2f;
    private float timer = 0f;
    private Vector3 destino;

    public Animator animator;

    [Header("Rango de exploración")]
    public Vector2 minExplorar = new Vector2(-5, -3);
    public Vector2 maxExplorar = new Vector2(5, 3);

    // Energía
    [Header("Energía")]
    public float energia = 100f;
    public float maxEnergia = 100f;
    public float consumoExplorar = 2f;
    public float consumoJugar = 4f;
    public float recuperacionContactoComida = 40f;
    public float energiaCritica = 20f;

    // ASUSTADO
    [Header("Asustado")]
    public float scaredSpeed = 6f;
    public float scaredDuration = 4f;
    public float scaredEnergyPerSecond = 8f;
    private float scaredTimer = 0f;

    bool contactoConComida = false;
    float tiempoBuscandoComida = 0f;

    public Transform cama;
    FoodSpawner foodSpawner;

    public event System.Action<AnimalState> OnStateChanged;
    public event System.Action<float> OnEnergyChanged;

    // Detectar movimiento REAL (para animación Stop/Walk)
    private Vector3 posicionAnterior;

    void Start()
    {
        posicionAnterior = transform.position;
        foodSpawner = FindObjectOfType<FoodSpawner>();
        SimulationManager sim = FindObjectOfType<SimulationManager>();
        if (sim != null) sim.RegisterAnimal(this);
        CambiarEstado(AnimalState.Quieto);
        OnEnergyChanged?.Invoke(energia);
    }

    public void Simulate(float dt)
    {
        // comportamiento por estado
        switch (currentState)
        {
            case AnimalState.Quieto: EstadoQuieto(dt); break;
            case AnimalState.Comer: EstadoComer(dt); break;
            case AnimalState.Jugar: EstadoJugar(dt); break;
            case AnimalState.Dormir: EstadoDormir(dt); break;
            case AnimalState.Explorar: EstadoExplorar(dt); break;
            case AnimalState.Asustado: EstadoAsustado(dt); break;
        }

        // Energía y eventos
        energia = Mathf.Clamp(energia, 0, maxEnergia);
        OnEnergyChanged?.Invoke(energia);
        RevisarTransiciones();

        // Detectar movimiento real -> animator Movement (0 o 1)
        float distanciaMovida = Vector3.Distance(transform.position, posicionAnterior);
        if (animator != null)
        {
            animator.SetFloat("Movement", distanciaMovida > 0.01f ? 1f : 0f);
        }
        posicionAnterior = transform.position;
    }

    // ----------------------
    // Estados
    // ----------------------
    void EstadoQuieto(float dt)
    {
        timer += dt;
        if (timer >= 2f)
        {
            timer = 0;

            // Dormir si la energía está baja (<55) con probabilidad parcial
            if (energia < 55f && Random.value < 0.35f)
            {
                CambiarEstado(AnimalState.Dormir);
                return;
            }

            int r = Random.value < 0.7f ? Random.Range(0, 2) : 2;
            if (energia > 70f)
            {
                if (r == 0) CambiarEstado(AnimalState.Jugar);
                else CambiarEstado(AnimalState.Explorar);
            }
            else
            {
                if (r == 0) CambiarEstado(AnimalState.Comer);
                else CambiarEstado(AnimalState.Explorar);
            }
        }
    }

    void EstadoComer(float dt)
    {
        MoverHacia(destino, dt);

        if (contactoConComida)
        {
            energia += recuperacionContactoComida * dt;
            if (energia >= maxEnergia * 0.9f)
                CambiarEstado(AnimalState.Quieto);
        }

        tiempoBuscandoComida += dt;
        if (tiempoBuscandoComida >= 4f && !contactoConComida)
            CambiarEstado(AnimalState.Quieto);
    }

    void EstadoJugar(float dt)
    {
        energia -= consumoJugar * dt;
        timer += dt;
        if (timer >= 4f)
        {
            timer = 0;
            CambiarEstado(AnimalState.Quieto);
        }
    }

    void EstadoDormir(float dt)
    {
        // Ir a cama si no está ya
        if (cama == null)
        {
            // si no hay cama, dormir en sitio
            energia += 10f * dt;
        }
        else
        {
            if (Vector3.Distance(transform.position, cama.position) > 0.2f)
            {
                MoverHacia(cama.position, dt);
                return;
            }
            else
            {
                energia += 10f * dt;
            }
        }

        if (energia >= maxEnergia * 0.95f)
            CambiarEstado(AnimalState.Quieto);
    }

    void EstadoExplorar(float dt)
    {
        energia -= consumoExplorar * dt;
        MoverHacia(destino, dt);
        if (Vector3.Distance(transform.position, destino) < 0.2f)
            CambiarEstado(AnimalState.Quieto);
    }

    void EstadoAsustado(float dt)
    {
        // El animal corre en una dirección aleatoria (cambia cada 0.4s)
        scaredTimer += dt;
        energia -= scaredEnergyPerSecond * dt;

        // movimiento rápido en dirección actual (destino se usa como dirección)
        MoverHacia(destino, dt);

        if (scaredTimer >= 0.4f)
        {
            scaredTimer = 0f;
            float rx = Random.Range(-1f, 1f);
            float ry = Random.Range(-1f, 1f);
            Vector3 randomDir = new Vector3(rx, ry, 0).normalized;

            destino = transform.position + randomDir * 2f;

            destino = new Vector3(
                Mathf.Clamp(destino.x, minExplorar.x, maxExplorar.x),
                Mathf.Clamp(destino.y, minExplorar.y, maxExplorar.y),
                0
            );
        }



    }

    void RevisarTransiciones()
    {
        if (energia <= 0 && currentState != AnimalState.Dormir)
        {
            CambiarEstado(AnimalState.Dormir);
        }
    }

    public void TocarComida()
    {
        contactoConComida = true;
        tiempoBuscandoComida = 0f;
    }

    // Método público para forzar el susto (puede llamarlo UI o ScareManager)
    public void TriggerScare(float duration = -1f)
    {
        // no sobreponer si ya asustado
        if (currentState == AnimalState.Asustado) return;

        SimulationManagerLifeCats.Instance.AddScare();

        // setear duración si viene
        if (duration > 0f) scaredDuration = duration;

        // guardar destino inicial: correr en dirección aleatoria
        Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
        destino = transform.position + randomDir * 2f;

        // cambiar estado
        CambiarEstado(AnimalState.Asustado);

        // ajustar animator
        if (animator != null)
            animator.SetBool("isScared", true);

        // iniciar contador de tiempo que dejará el estado asustado
        Invoke(nameof(StopScare), scaredDuration);
    }

    void StopScare()
    {
        // Termina el estado asustado: volver a Quieto
        if (currentState == AnimalState.Asustado)
        {
            CambiarEstado(AnimalState.Quieto);
            if (animator != null)
                animator.SetBool("isScared", false);
        }
    }

    void CambiarEstado(AnimalState nuevo)
    {
        if (currentState == nuevo) return;

        AnimalState estadoAnterior = currentState;

        // reset temporales
        timer = 0;
        contactoConComida = false;
        tiempoBuscandoComida = 0f;
        scaredTimer = 0f;

        // Si salimos de Comer → destruir comida
        if (estadoAnterior == AnimalState.Comer && foodSpawner != null)
            foodSpawner.DestruirComida();

        // Aplicar nuevo estado
        currentState = nuevo;

        // Lógica por entrada
        if (nuevo == AnimalState.Comer)
        {
            if (foodSpawner != null)
            {
                foodSpawner.SpawnFood();
                GameObject comida = foodSpawner.GetComida();
                if (comida != null)
                    destino = comida.transform.position;
            }
        }
        else if (nuevo == AnimalState.Explorar)
        {
            destino = new Vector3(
                Random.Range(minExplorar.x, maxExplorar.x),
                Random.Range(minExplorar.y, maxExplorar.y),
                0
            );
        }
        else if (nuevo == AnimalState.Dormir)
        {
            if (cama != null)
                destino = cama.position;
        }
        else if (nuevo == AnimalState.Asustado)
        {
            // velocidad temporalmente más alta
            // damos destino ya en TriggerScare o aquí si se llama directo
        }

        // ANIMATOR: mantener parámetros limpios
        if (animator != null)
        {
            animator.SetBool("isPlaying", nuevo == AnimalState.Jugar);
            animator.SetBool("isSleeping", nuevo == AnimalState.Dormir);
            animator.SetBool("isScared", nuevo == AnimalState.Asustado);

            // si no se mueve -> Movement se actualizará en Simulate, aquí forzamos 0 si quieto/dormir
            if (nuevo == AnimalState.Quieto || nuevo == AnimalState.Dormir)
                animator.SetFloat("Movement", 0f);
        }

        OnStateChanged?.Invoke(currentState);
    }

    void MoverHacia(Vector3 target, float dt)
    {
        Vector3 dir = (target - transform.position);

        // Si ya llegó → no moverse más
        if (dir.magnitude < 0.05f) return;

        // Flip izquierdo/derecho
        if (dir.x > 0.01f) transform.localScale = new Vector3(1, 1, 1);
        else if (dir.x < -0.01f) transform.localScale = new Vector3(-1, 1, 1);

        // Si está asustado usar velocidad mayor
        float s = (currentState == AnimalState.Asustado) ? scaredSpeed : moveSpeed;

        Vector3 moveDir = dir.normalized;
        transform.position += moveDir * s * dt;
    }

    void Asustar()
    {
        if (currentState == AnimalState.Asustado) return;

        animator.SetBool("IsScared", true);
        CambiarEstado(AnimalState.Asustado);
    }

}
