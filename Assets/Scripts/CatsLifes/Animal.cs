using UnityEngine;

public class Animal : MonoBehaviour
{
    public AnimalState currentState = AnimalState.Quieto;

    public float moveSpeed = 2f;
    private float timer = 0f;
    private Vector3 destino;

    public Animator animator;

    [Header("Rango de exploración")]
    public Vector2 minExplorar = new Vector2(-5, -3);
    public Vector2 maxExplorar = new Vector2(5, 3);


    // Energía
    public float energia = 100f;
    public float maxEnergia = 100f;

    public float consumoExplorar = 2f;
    public float consumoJugar = 4f;

    public float recuperacionContactoComida = 40f;

    public float energiaCritica = 20f;

    bool contactoConComida = false;
    float tiempoBuscandoComida = 0f;



    FoodSpawner foodSpawner;

    public event System.Action<AnimalState> OnStateChanged;
    public event System.Action<float> OnEnergyChanged;


    void Start()
    {
        foodSpawner = FindObjectOfType<FoodSpawner>();

        SimulationManager sim = FindObjectOfType<SimulationManager>();
        if (sim != null) sim.RegisterAnimal(this);

        CambiarEstado(AnimalState.Quieto);
        OnEnergyChanged?.Invoke(energia);
    }


    public void Simulate(float dt)
    {
        switch (currentState)
        {
            case AnimalState.Quieto: EstadoQuieto(dt); break;
            case AnimalState.Comer: EstadoComer(dt); break;
            case AnimalState.Jugar: EstadoJugar(dt); break;
            case AnimalState.Dormir: EstadoDormir(dt); break;
            case AnimalState.Explorar: EstadoExplorar(dt); break;
        }

        energia = Mathf.Clamp(energia, 0, maxEnergia);
        OnEnergyChanged?.Invoke(energia);
        RevisarTransiciones();
    }

    // =====================================================
    //                    ESTADOS
    // =====================================================

    void EstadoQuieto(float dt)
    {
        timer += dt;

        if (timer >= 2f)
        {
            timer = 0;

            if (energia < energiaCritica)
            {
                CambiarEstado(AnimalState.Dormir);
                return;
            }

            int r = Random.value < 0.7f ? Random.Range(0, 2) : 2;
            // 70% Comer/Jugar, 30% Explorar

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
        // Mover hacia la comida
        MoverHacia(destino, dt);

        // Si toca comida, recupera energía
        if (contactoConComida)
        {
            energia += recuperacionContactoComida * dt;

            if (energia >= maxEnergia * 0.9f)
                CambiarEstado(AnimalState.Quieto);
        }

        // Si pasan 4 segundos sin encontrar comida → cancelar
        tiempoBuscandoComida += dt;
        if (tiempoBuscandoComida >= 4f && !contactoConComida)
        {
            CambiarEstado(AnimalState.Quieto);
        }
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
        energia += 10f * dt;

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

    // =====================================================
    //                  TRANSICIONES
    // =====================================================

    void RevisarTransiciones()
    {
        if (energia <= 0 && currentState != AnimalState.Dormir)
        {
            CambiarEstado(AnimalState.Dormir);
        }
    }

    // =====================================================
    //                  UTILIDADES
    // =====================================================

    public void TocarComida()
    {
        contactoConComida = true;
        tiempoBuscandoComida = 0f;
    }

    void CambiarEstado(AnimalState nuevo)
    {
        if (currentState == nuevo) return;

        // Guardamos el estado anterior solo para comparación
        AnimalState estadoAnterior = currentState;

        timer = 0;
        contactoConComida = false;
        tiempoBuscandoComida = 0f;

        // Si salimos de comer → destruir comida
        if (estadoAnterior == AnimalState.Comer && foodSpawner != null)
            foodSpawner.DestruirComida();

        // Aplicamos el nuevo estado
        currentState = nuevo;

        // ------------------------------
        //       ANIMACIÓN
        // ------------------------------
        

        // ------------------------------
        //       LÓGICA DE ESTADOS
        // ------------------------------

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

        if (nuevo == AnimalState.Explorar)
        {
            destino = new Vector3(
                Random.Range(minExplorar.x, maxExplorar.x),
                Random.Range(minExplorar.y, maxExplorar.y),
                0
            );
        }

        OnStateChanged?.Invoke(currentState);
    }




    void MoverHacia(Vector3 target, float dt)
    {
        Vector3 dir = (target - transform.position);

        // --- Animación de movimiento ---
        if (animator != null)
            animator.SetFloat("Movement", dir.magnitude);  // 0 = quieto, >0 = moverse

        // --- Rotación izquierda-derecha ---
        if (dir.x > 0.01f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (dir.x < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // --- Movimiento ---
        Vector3 moveDir = dir.normalized;
        transform.position += moveDir * moveSpeed * dt;
    }


}

