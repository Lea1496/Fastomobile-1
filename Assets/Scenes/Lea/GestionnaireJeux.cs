using System;
using System.Collections;
using System.Collections.Generic;

using System.Threading;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



[RequireComponent(typeof(ScriptSpline))]
[RequireComponent(typeof(CréateurDébutPartie))]
public class GestionnaireJeux : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int largeur;  
    public GameObject terrain;
    [SerializeField] private GameObject point;
    [SerializeField] private GameObject point1;
    [SerializeField] public GameObject obstalce1; // à changer
    [SerializeField] private GameObject obstacle2; // à changer
    [SerializeField] private Camera cam1;
    [SerializeField] private Camera cam2;
    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject bonus;
    [SerializeField] private GameObject checkpoint;
    [SerializeField] private Text textCoin;
    [SerializeField] private Text textRang;
    [SerializeField] private Text textRg;
    [SerializeField] private Text textVie;
    [SerializeField] private Text textCoin2;
    [SerializeField] private Text textRang2;
    [SerializeField] private Text textRg2;
    [SerializeField] private Text textVie2;
    [SerializeField] private Text textLaps;
    [SerializeField] private Text textLaps2;
    [SerializeField] private Text textFinish;
    [SerializeField] private Text textFinish2;
    [SerializeField] private Text textGameOver;
    [SerializeField] private Text textGameOver2;
    [SerializeField] private Speedometer speedometer;
    [SerializeField] private Speedometer speedometer2;
    [SerializeField] private Canvas speedometerCanv;
    [SerializeField] private Canvas speedometerCanv2;
    [SerializeField] private GameObject playerData2;

    private ScriptSpline créerRoute;
    private List<Vector3> chemin;
    private GameObject mainPlayer1;
    private GameObject mainPlayer2;
    private List<PlayerData> autos;
    private Vector3[] sommets;
    private CréateurDébutPartie créateur;
    private Vector3 desiredPos;
    private Vector3 desiredPos2;
    private GameObject ligne;
    private Transform target;
    private Transform target2;
    private Player mainPlayer1Live;
    private Player mainPlayer2Live;
    GameObject checkpointInst;
    private bool isGameOver1 = false;
    private bool isGameOver2 = false;
    private MeshRenderer[] renderers;
    private Collider[] colliders;
    private MeshRenderer[] renderers2;
    private Collider[] colliders2;
    private Rigidbody rg1;
    private Rigidbody rg2;
    private string gameOver = "Game Over!";
    private string finish = "Finish!";
    private int compteur = 0;
    private bool existsMainPlayer2 = false;
    private float temps = 0;
    private GestionnaireTouches gestion1;
    private GestionnaireTouches gestion2;
    
    public Player MainPlayer1
    {
        get => mainPlayer1.GetComponent<Player>();
    }
    public Player MainPlayer2
    {
        get => mainPlayer2.GetComponent<Player>();
    }
    public List<Vector3> Chemin
    {
        get => chemin;
    }
    public GameObject Coin
    {
        get => coin;
    }
    public List<PlayerData> Autos
    {
        get => autos;
    }
    
    // les deux prochaine fonctions viennent de
    // https://stackoverflow.com/questions/4501838/terminate-a-thread-after-an-interval-if-not-returned
    
    private void WorkThreadFunction()
    {
        try
        {
            chemin = new CréateurChemin(largeur).ListePos;
        }
        catch 
        {
            Refaire();
        }
    }
    private void Refaire()
    {
        Thread thread = new Thread(new ThreadStart(WorkThreadFunction)) ;
       
        thread.Start();
        if (!thread.Join(new TimeSpan(0, 0, 1)) && chemin == null)
        {
            thread.Abort();
            
            Refaire();
            
        }
    }

    void Awake()
   {
       
       créerRoute = GetComponent<ScriptSpline>();
       Refaire();
       
        créateur = GetComponent<CréateurDébutPartie>();
        chemin = new ScriptBézier(chemin).PointsSpline;
        créerRoute.FaireMesh(chemin);
        sommets = créerRoute.sommets;
        
        
        if (GameData.P2.IsMainPlayer)
        {
            cam2.gameObject.SetActive(true);
            cam1.rect = new Rect(0.5f, 0, 0.5f, 1);
            playerData2.SetActive(true);
        }
        
        //Instancie les checkpoints
        for (int i = 2; i < chemin.Count -2; i++)
        {
            checkpointInst = Instantiate(checkpoint, Vector3.zero, 
                checkpoint.transform.rotation);
            checkpointInst.GetComponentInChildren<GénérateurCheckPoints>().FaireMesh(i* 2, sommets);
            checkpointInst.GetComponentInChildren<RankingManager>().indiceCheckpoint = chemin.Count -1 -i;
            checkpointInst.GetComponentInChildren<RankingManager>().indiceMax = chemin.Count;
            // DontDestroyOnLoad(checkpointInst);
        }
        
        //Instancie ligne d'arrivée
        gameObject.GetComponentInChildren<CréateurLigneArrivée>().FaireMesh(new Vector3(chemin[chemin.Count -1].x, 0, chemin[chemin.Count -1].z +70),new Vector3(chemin[chemin.Count -1].x, 0, chemin[chemin.Count -1].z -70));
        
        //Instancie les coins, obstacles et bonus
        new GénérateurObjets().GénérerObjets(obstalce1, obstacle2, coin, bonus, sommets);
        
        //Crée la liste de joueurs
        autos = new GestionnairePlayer().Joueurs;
        
        //Crée le début de la partie
        créateur.CréerDébutPartie(autos, chemin, sommets);
       
        /*TerrainData theTerrain = new Object2Terrain().CreateTerrain1(gameObject);
        GameObject terrainObject = Terrain.CreateTerrainGameObject(theTerrain);
        terrainObject.transform.SetLocalPositionAndRotation(new Vector3(-68,0,-68), terrainObject.transform.rotation);*/
        
        existsMainPlayer2 = GameData.P2.IsMainPlayer;

        mainPlayer1 = créateur.MainPlayer1;
        mainPlayer2 = créateur.MainPlayer2;
        mainPlayer1Live = mainPlayer1.GetComponent<Player>();
        rg1 = mainPlayer1.GetComponent<Rigidbody>();
        renderers = mainPlayer1.GetComponentsInChildren<MeshRenderer>();
        colliders = mainPlayer1.GetComponentsInChildren<Collider>();
        gestion1 = mainPlayer1.GetComponent<GestionnaireTouches>();

        if (existsMainPlayer2)
        {
            mainPlayer2Live = mainPlayer2.GetComponent<Player>();
            rg2 = mainPlayer2.GetComponent<Rigidbody>();
            renderers2 = mainPlayer2.GetComponentsInChildren<MeshRenderer>();
            colliders2 = mainPlayer2.GetComponentsInChildren<Collider>();
            gestion2 = mainPlayer2.GetComponent<GestionnaireTouches>();
            //instancier son background
        }
        else
        {
            //instancier sons 
            //cam1.AudioListener
        }
        
   }

    private void Start()
    {
        new CréateurTerrain(largeur, terrain);
    }

    private void LateUpdate()
    {
        if (Keyboard.current.rKey.IsPressed())
        {
            GameObject.FindGameObjectWithTag("Bouton").GetComponent<GestionBoutons>().NouvellePartie();
        }
        temps += Time.deltaTime;
        
        //Fait les deux premiers checkpoints
       /* if (temps >= 20 && compteur < 1)
        {
            checkpointInst = Instantiate(checkpoint, Vector3.zero, 
                checkpoint.transform.rotation);
            checkpointInst.GetComponentInChildren<GénérateurCheckPoints>().FaireMesh(0, sommets);
            checkpointInst.GetComponentInChildren<RankingManager>().indiceCheckpoint = chemin.Count - 1;
            checkpointInst.GetComponentInChildren<RankingManager>().indiceMax = chemin.Count;
            checkpointInst = Instantiate(checkpoint, Vector3.zero, 
                checkpoint.transform.rotation);
            checkpointInst.GetComponentInChildren<GénérateurCheckPoints>().FaireMesh(2, sommets);
            checkpointInst.GetComponentInChildren<RankingManager>().indiceCheckpoint = chemin.Count - 2;
            checkpointInst.GetComponentInChildren<RankingManager>().indiceMax = chemin.Count;
            compteur++;
        }*/
        
       //mainPlayer1Live = mainPlayer1.GetComponent<Player>(); //NÉCESSAIRE??
       
       
        speedometer.speed = (int)Math.Floor(rg1.velocity.magnitude) * 2.237f;
        if (!isGameOver1)
        {
            if (mainPlayer1Live.IsFinished)
            {
                ChangerAffichageÉcran(mainPlayer1, cam1, 1, finish, textFinish);
                gestion1.enabled = false;
                for (int i = 0; i < colliders.Length; i++)
                {
                    colliders[i].enabled = false;
                }

                for (int i = 0; i < renderers.Length; i++)
                {
                    renderers[i].enabled = false;
                }

                rg1.useGravity = false;
                
                if (isGameOver2)
                {
                    StartCoroutine(FinirPartie());
                    StopCoroutine(FinirPartie());
                }
            }
            else
            {
                if (mainPlayer1Live.Vie <= 0)
                {
                    ChangerAffichageÉcran(mainPlayer1, cam1, 1, gameOver, textGameOver);
                    gestion1.enabled = false;
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        colliders[i].enabled = false;
                    }

                    for (int i = 0; i < renderers.Length; i++)
                    {
                        renderers[i].enabled = false;
                    }

                    rg1.useGravity = false;
                    
                    isGameOver1 = true;
                    
                }
            }
   
            GérerCaméra(mainPlayer1Live, cam1);
   
            textCoin.text = $"Coin: {mainPlayer1Live.Argent.ToString()}";
            textRang.text = mainPlayer1Live.Rang.ToString();
            textVie.text = $"HP: {mainPlayer1Live.Vie.ToString()}";
            textLaps.text = $"{mainPlayer1Live.Tour}/3";
        }
       
       
       

       if (existsMainPlayer2) 
       {
           //mainPlayer2Live = mainPlayer2.GetComponent<Player>(); //NÉCESSAIRE??
           speedometer2.speed = (int)Math.Floor(rg2.velocity.magnitude) * 2.237f;
           if (!isGameOver2)
           {
               if (mainPlayer2Live.IsFinished)
               {
                   ChangerAffichageÉcran(mainPlayer2, cam2, 2, finish, textFinish2);
                   gestion2.enabled = false;
                   for (int i = 0; i < colliders.Length; i++)
                   {
                       colliders[i].enabled = false;
                   }

                   for (int i = 0; i < renderers.Length; i++)
                   {
                       renderers[i].enabled = false;
                   }

                   rg1.useGravity = false;
                   
                   if (isGameOver1)
                   {
                       StartCoroutine(FinirPartie());
                       StopCoroutine(FinirPartie());
                   }
               }
               else
               {
                   if (mainPlayer2Live.Vie <= 0)
                   {
                       ChangerAffichageÉcran(mainPlayer2, cam2, 2, gameOver, textGameOver2);
                       gestion2.enabled = false;
                       for (int i = 0; i < colliders.Length; i++)
                       {
                           colliders2[i].enabled = false;
                       }

                       for (int i = 0; i < renderers.Length; i++)
                       {
                           renderers2[i].enabled = false;
                       }

                       rg2.useGravity = false;
                       
                       isGameOver2 = true;
                       if (mainPlayer1Live.IsFinished)
                       {
                           StartCoroutine(FinirPartie());
                           StopCoroutine(FinirPartie());
                       }

                   }   
               }

               GérerCaméra(mainPlayer2Live, cam2);
          
               textCoin2.text = $"Coin: {mainPlayer2Live.Argent.ToString()}";
               textRang2.text = mainPlayer2Live.Rang.ToString();
               textVie2.text = $"HP: {mainPlayer2Live.Vie.ToString()}";
               textLaps2.text = $"{mainPlayer2Live.Tour}/3";
           }
           
           
           if (mainPlayer1Live.Vie == 0 && mainPlayer2Live.Vie == 0)
           {
               StartCoroutine(FinirPartie());
               StopCoroutine(FinirPartie());
           }

           if (mainPlayer1Live.IsFinished && mainPlayer2Live.IsFinished)
           {
               StartCoroutine(FinirPartie());
               StopCoroutine(FinirPartie());
           }
       }

       if (isGameOver1 && !GameData.P2.IsMainPlayer )
       {
           SceneManager.LoadScene(5);
       }

       
   }

   //Cette fonction vient de :https://github.com/bhavik66/Unity3D-Ranking-System/tree/master/Assets/RankingSystem/Scripts
   private void GérerCaméra(Player mainPlayer, Camera cam)
   {
       // Calculate the current rotation angles
       Transform target = mainPlayer.transform;
       float wantedRotationAngle = target.eulerAngles.y;
       float wantedHeight = target.position.y + 20;

       float  currentRotationAngle = cam.transform.eulerAngles.y;
       float currentHeight = cam.transform.position.y;

       // Damp the rotation around the y-axis
       currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, 3f * Time.deltaTime);

       // Damp the height
       currentHeight = Mathf.Lerp(currentHeight, wantedHeight, 2f * Time.deltaTime);

       // Convert the angle into a rotation
       Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

       // Set the position of the camera on the x-z plane to:
       // distance meters behind the target
       cam.transform.position = target.position;
       cam.transform.position -= currentRotation * Vector3.forward * 50;

       cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, currentRotation, 3f * Time.deltaTime);

       // Set the height of the camera
       cam.transform.position = new Vector3(cam.transform.position.x, currentHeight, cam.transform.position.z);

       // Always look at the target
       cam.transform.LookAt(target);
   }
   private IEnumerator FinirPartie()
   {
       yield return new WaitForSeconds(1f);
       SceneManager.LoadScene(4);
   }
   private void ChangerAffichageÉcran(GameObject player, Camera cam, int indice, string action, Text text)
   {
       
       cam.clearFlags = CameraClearFlags.SolidColor;
       DésactiverTextes(indice);
       cam.cullingMask = 0;
       text.text = action;
       if (existsMainPlayer2)
       {
           text.alignment = TextAnchor.MiddleRight;
       }
       
    }

   private void DésactiverTextes(int indice)
   {
       if (indice == 1)
       {
           textCoin.enabled = false;
           textRang.enabled = false;
           textVie.enabled = false;
           textLaps.enabled = false;
           textRg.enabled = false;
           speedometerCanv.enabled = false;
       }
       else
       {
           textCoin2.enabled = false;
           textRang2.enabled = false;
           textVie2.enabled = false;
           textLaps2.enabled = false;
           textRg2.enabled = false;
           speedometerCanv2.enabled = false;
       }
   }
   
   
}
