using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(SplineInterpolator))]
public class PeopleMover : Conveyance
{

    //list of points from grasshopper
    private List<float> ptx = new List<float>()
    {
        0.119277f,-1.762522f,-3.64432f,-5.526119f,-7.407918f,-9.289716f,-11.171515f,-13.053314f,-14.935112f,-16.816911f,-18.690501f,-20.539963f,-22.354824f,-24.125375f,-25.842588f,-27.498061f,-29.083925f,-30.592772f,-32.017581f,-33.351651f,-34.588537f,-35.722012f,-36.746027f,-37.65469f,-38.442266f,-39.103191f,-39.632107f,-40.023925f,-40.273913f,-40.377808f,-40.380723f,-40.380723f,-40.380723f,-40.380723f,-40.380723f,-40.380723f,-40.380723f,-40.380723f,-40.380723f,-41.117647f,-42.934823f,-43.380723f,-43.380723f,-43.380723f,-43.380723f,-43.380723f,-43.380723f,-43.380723f,-43.380723f,-43.380723f,-43.370704f,-43.251456f,-42.999785f,-42.618518f,-42.111233f,-41.481823f,-40.734246f,-39.872948f,-38.902525f,-37.827694f,-36.65345f,-35.384946f,-34.027505f,-32.586695f,-31.068301f,-29.478358f,-27.823224f,-26.109541f,-24.344332f,-22.535067f,-20.689535f,-18.816081f,-16.923517f,-15.040962f,-13.159163f,-11.277365f,-9.395566f,-7.513768f,-5.631969f,-3.75017f,-1.868372f,0.013427f,1.861252f,2.989862f,2.925202f,1.694148f,-0.192119f,-2.161509f,-4.130899f,-6.100289f,-8.069679f,-10.039069f,-12.008459f,-13.977849f,-15.947239f,-17.916137f,-19.87488f,-21.805758f,-23.691027f,-25.513379f,-27.25606f,-28.90306f,-30.439262f,-31.85054f,-33.123939f,-34.247746f,-35.211649f,-36.006789f,-36.625863f,-37.063182f,-37.314729f,-37.380723f,-37.380723f,-37.380723f,-37.380723f,-37.380723f,-37.380723f,-37.380723f,-37.380723f,-37.380723f,-37.757081f,-39.555007f,-40.380723f,-40.380723f,-40.380723f,-40.380723f,-40.380723f,-40.380723f,-40.380723f,-40.380723f,-40.380723f,-40.37913f,-40.282846f,-40.040266f,-39.655623f,-39.13363f,-38.47936f,-37.698157f,-36.79558f,-35.777348f,-34.649351f,-33.417628f,-32.088398f,-30.66809f,-29.163392f,-27.581309f,-25.929227f,-24.214985f,-22.446962f,-20.634142f,-18.786208f,-16.913589f,-15.031805f,-13.150007f,-11.268208f,-9.386409f,-7.504611f,-5.622812f,-3.741014f,-1.859215f,0.022584f,1.540594f,0.945326f,-0.926627f,-2.808426f,-4.690224f,-6.572023f,-8.453822f,-10.33562f,-12.217419f,-14.099217f,-15.981016f,-17.870771f,-19.754914f,-21.615497f,-23.443912f,-25.232124f,-26.972504f,-28.657788f,-30.281166f,-31.836145f,-33.316519f,-34.716387f,-36.030083f,-37.252154f,-38.377368f,-39.400653f,-40.317141f,-41.122196f,-41.811271f,-42.380155f,-42.824977f,-43.141889f,-43.327752f,-43.380723f,-43.380723f,-43.380723f,-43.380723f,-43.380723f,-43.380723f,-43.380723f,-43.380723f,-43.380723f,-43.367174f,-42.065291f,-40.48841f,-40.380723f,-40.380723f,-40.380723f,-40.380723f,-40.380723f,-40.380723f,-40.380723f,-40.380723f,-40.380723f,-40.344413f,-40.167039f,-39.845635f,-39.384713f,-38.789197f,-38.064319f,-37.215548f,-36.248534f,-35.169095f,-33.983191f,-32.696966f,-31.316743f,-29.849096f,-28.30088f,-26.67929f,-24.99196f,-23.247014f,-21.453157f,-19.619748f,-17.756878f,-15.876857f,-13.995058f,-12.113259f,-10.231461f,-8.349662f,-6.467864f,-4.586065f,-2.704266f,-0.822468f,1.103087f,3.072477f,5.041867f,7.011257f,8.980647f,10.950037f,12.919427f,14.888817f,16.858207f,18.826335f,20.784845f,22.722967f,24.630032f,26.495539f,28.309221f,30.061093f,31.74152f,33.341249f,34.851478f,36.263887f,37.570708f,38.764747f,39.839422f,40.788832f,41.607749f,42.291659f,42.836801f,43.240174f,43.49956f,43.613529f,43.619277f,43.619277f,43.619277f,43.619277f,43.619277f,43.619277f,43.619277f,43.619277f,43.619277f,43.619277f,43.619277f,43.619277f,43.619277f,43.619277f,43.619277f,43.619252f,43.543525f,43.32214f,42.956316f,42.448065f,41.800185f,41.016243f,40.100553f,39.058155f,37.894787f,36.616853f,35.231385f,33.746007f,32.168896f,30.508747f,28.774677f,26.976238f,25.123329f,23.22615f,21.295141f,19.340931f,17.374279f,15.404897f,13.435507f,11.466117f,9.496727f,7.527337f,5.557947f,3.588557f,1.619167f,-0.350223f,-2.319613f,-4.289003f,-6.258393f,-8.227783f,-10.197173f,-12.166563f,-14.135953f,-16.105343f,-18.074289f,-20.036352f,-21.980834f,-23.897031f,-25.774389f,-27.602584f,-29.371543f,-31.071542f,-32.693211f,-34.227634f,-35.66636f,-37.001474f,-38.225627f,-39.332072f,-40.314729f,-41.168188f,-41.887747f,-42.46945f,-42.910093f,-43.207252f,-43.359291f,-43.380723f,-43.380723f,-43.380723f,-43.380723f,-43.380723f,-43.380723f,-43.380723f,-43.380723f,-43.380723f,-43.380723f,-43.380723f,-43.380723f,-43.380723f,-43.380723f,-43.380723f,-43.380723f,-43.380723f,-43.380723f,-43.333711f,-43.154657f,-42.844411f,-42.406068f,-41.843464f,-41.16047f,-40.361261f,-39.450388f,-38.432476f,-37.312376f,-36.095155f,-34.786038f,-33.39046f,-31.914081f,-30.362788f,-28.742769f,-27.060503f,-25.322778f,-23.536845f,-21.710302f,-19.851167f,-17.968018f,-16.077709f,-14.19591f,-12.314112f,-10.432313f,-8.550515f,-6.668716f,-4.786917f,-2.905119f,-1.02332f,0.859043f,1.56968f,0.119277f
    };

    private List<float> pty = new List<float>()
    {
        29f,29.580802f,30.161604f,30.742406f,31.323208f,31.90401f,32.484812f,33.065614f,33.646416f,34.227218f,34.827934f,35.466543f,36.138538f,36.839487f,37.565056f,38.311012f,39.073221f,39.847639f,40.630288f,41.417245f,42.204611f,42.98849f,43.764965f,44.530072f,45.279782f,46.009982f,46.71646f,47.394903f,48.040898f,48.649948f,49.231566f,49.812368f,50.39317f,50.973972f,51.554775f,52.135577f,52.716379f,53.297181f,53.877983f,54.0f,54.0f,54.230636f,54.811438f,55.39224f,55.973042f,56.553844f,57.134646f,57.715448f,58.29625f,58.877052f,59.43703f,59.986264f,60.565685f,61.172139f,61.802427f,62.453379f,63.1219f,63.804898f,64.499323f,65.202168f,65.910441f,66.621172f,67.331399f,68.038156f,68.738468f,69.429338f,70.107745f,70.770626f,71.414887f,72.03741f,72.635005f,73.204493f,73.742704f,74.320914f,74.901716f,75.482518f,76.06332f,76.644122f,77.224924f,77.805726f,78.386528f,78.96733f,79.0f,79.0f,79f,79f,79f,79.0f,79f,79f,79f,79f,79f,79f,79f,79f,79.0f,79.0f,79.0f,79f,79f,79f,79f,79f,79.0f,79f,79f,79f,79f,79.0f,79.0f,79f,79f,79f,79f,79f,79f,79f,79f,79f,79f,79f,78.907826f,78.327024f,77.746222f,77.16542f,76.584618f,76.003816f,75.423014f,74.842212f,74.26141f,73.680161f,73.073131f,72.428926f,71.752041f,71.046898f,70.317814f,69.569005f,68.804592f,68.028602f,67.245008f,66.457726f,65.670653f,64.887687f,64.112749f,63.349815f,62.602925f,61.876206f,61.173887f,60.500296f,59.859862f,59.257091f,58.67626f,58.095458f,57.514656f,56.933854f,56.353052f,55.77225f,55.191448f,54.610646f,54.029844f,54f,54f,53.67719f,53.096388f,52.515586f,51.934784f,51.353982f,50.77318f,50.192378f,49.611576f,49.030774f,48.477952f,47.923714f,47.339795f,46.729344f,46.095567f,45.441617f,44.770594f,44.085582f,43.389619f,42.685714f,41.976853f,41.266004f,40.55613f,39.850197f,39.151183f,38.462088f,37.78594f,37.125806f,36.484787f,35.866004f,35.272659f,34.707921f,34.167714f,33.586912f,33.00611f,32.425308f,31.844506f,31.263704f,30.682902f,30.1021f,29.521298f,29.0f,29f,29.0f,28.587843f,28.007041f,27.426238f,26.845436f,26.264634f,25.683832f,25.10303f,24.522228f,23.941426f,23.350597f,22.722525f,22.059764f,21.366771f,20.64791f,19.907443f,19.149533f,18.378248f,17.597585f,16.811474f,16.023826f,15.23853f,14.4595f,13.690682f,12.936081f,12.199787f,11.485982f,10.798948f,10.143071f,9.522827f,8.937078f,8.356276f,7.775474f,7.194672f,6.61387f,6.033068f,5.452266f,4.871464f,4.290662f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4.0f,4f,4f,4f,4f,4f,4.0f,4f,4.0f,4f,4f,4.0f,4f,4.0f,4f,4f,4.0f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4.0f,4f,4f,4.0f,4f,4.0f,4f,4f,4.0f,4f,4f,4f,4f,4.0f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4.0f,4f,4.0f,4.0f,4f,4.0f,4f,4.0f,4f,4.0f,4.0f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4f,4.491454f,5.072256f,5.653059f,6.233861f,6.814663f,7.395465f,7.976267f,8.557069f,9.137871f,9.679733f,10.242915f,10.834867f,11.452423f,12.092377f,12.751602f,13.427004f,14.115507f,14.814085f,15.519735f,16.229479f,16.94035f,17.649386f,18.353618f,19.050062f,19.735711f,20.407527f,21.062431f,21.697319f,22.309047f,22.894407f,23.450248f,24.00093f,24.581732f,25.162534f,25.743336f,26.324138f,26.90494f,27.485743f,28.066545f,28.647347f,29.0f,29f,29f
    };

    private List<float> ptz = new List<float>()
    {
        -40.453878f,-40.453878f,-40.453878f,-40.453878f,-40.453878f,-40.453878f,-40.453878f,-40.453878f,-40.453878f,-40.453878f,-40.381149f,-40.161518f,-39.799117f,-39.298591f,-38.664963f,-37.903538f,-37.019843f,-36.019579f,-34.908603f,-33.692931f,-32.378752f,-30.972459f,-29.480695f,-27.910412f,-26.268927f,-24.564005f,-22.803933f,-20.997596f,-19.154558f,-17.285133f,-15.403603f,-13.521805f,-11.640006f,-9.758208f,-7.876409f,-5.99461f,-4.112812f,-2.231013f,-0.349215f,1.337522f,1.113302f,-0.701139f,-2.582938f,-4.464737f,-6.346535f,-8.228334f,-10.110132f,-11.991931f,-13.87373f,-15.755528f,-17.643335f,-19.530401f,-21.395311f,-23.229379f,-25.024489f,-26.772961f,-28.467445f,-30.101089f,-31.667352f,-33.159961f,-34.572989f,-35.900735f,-37.1377f,-38.278624f,-39.318419f,-40.252175f,-41.075211f,-41.782987f,-42.371219f,-42.835983f,-43.173423f,-43.380269f,-43.453752f,-43.453878f,-43.453878f,-43.453878f,-43.453878f,-43.453878f,-43.453878f,-43.453878f,-43.453878f,-43.453878f,-42.89632f,-41.32551f,-39.392379f,-37.900492f,-37.453878f,-37.453878f,-37.453878f,-37.453878f,-37.453878f,-37.453878f,-37.453878f,-37.453878f,-37.453878f,-37.425748f,-37.228652f,-36.844936f,-36.278126f,-35.533426f,-34.61768f,-33.539304f,-32.308195f,-30.935672f,-29.434334f,-27.817992f,-26.101479f,-24.30057f,-22.431807f,-20.512359f,-18.559859f,-16.592236f,-14.622846f,-12.653456f,-10.684066f,-8.714676f,-6.745286f,-4.775896f,-2.806506f,-0.837116f,1.039816f,1.386025f,-0.252521f,-2.13432f,-4.016119f,-5.897917f,-7.779716f,-9.661514f,-11.543313f,-13.425112f,-15.30691f,-17.188563f,-19.059056f,-20.903702f,-22.712166f,-24.474837f,-26.182798f,-27.827737f,-29.401858f,-30.897832f,-32.308681f,-33.627751f,-34.848637f,-35.965141f,-36.971239f,-37.861064f,-38.628906f,-39.269232f,-39.776722f,-40.146338f,-40.373412f,-40.453768f,-40.453878f,-40.453878f,-40.453878f,-40.453878f,-40.453878f,-40.453878f,-40.453878f,-40.453878f,-40.453878f,-41.474443f,-43.205934f,-43.453878f,-43.453878f,-43.453878f,-43.453878f,-43.453878f,-43.453878f,-43.453878f,-43.453878f,-43.453878f,-43.433887f,-43.293436f,-43.021003f,-42.619487f,-42.092557f,-41.444105f,-40.67819f,-39.799266f,-38.811943f,-37.721003f,-36.531453f,-35.248468f,-33.877419f,-32.42391f,-30.893747f,-29.293021f,-27.628155f,-25.90581f,-24.133098f,-22.317548f,-20.467013f,-18.589928f,-16.697273f,-14.815474f,-12.933676f,-11.051877f,-9.170078f,-7.28828f,-5.406481f,-3.524682f,-1.642884f,0.247281f,1.534723f,0.604211f,-1.289269f,-3.171067f,-5.052866f,-6.934664f,-8.816463f,-10.698262f,-12.58006f,-14.461859f,-16.343657f,-18.221633f,-20.079214f,-21.905153f,-23.689529f,-25.423125f,-27.097358f,-28.704215f,-30.236165f,-31.686076f,-33.04717f,-34.312926f,-35.477062f,-36.533473f,-37.476225f,-38.299542f,-38.997804f,-39.565588f,-39.99772f,-40.289354f,-40.436075f,-40.453878f,-40.453878f,-40.453878f,-40.453878f,-40.453878f,-40.453878f,-40.453878f,-40.453878f,-40.453878f,-40.453878f,-40.453878f,-40.453878f,-40.453878f,-40.453878f,-40.453878f,-40.453878f,-40.453878f,-40.453878f,-40.396365f,-40.194022f,-39.847033f,-39.357308f,-38.727544f,-37.961208f,-37.062518f,-36.036418f,-34.888557f,-33.625249f,-32.253455f,-30.780719f,-29.215148f,-27.565374f,-25.840457f,-24.049892f,-22.203548f,-20.311581f,-18.384412f,-16.432633f,-14.466998f,-12.49765f,-10.528259f,-8.558873f,-6.589483f,-4.620094f,-2.650706f,-0.681316f,1.288073f,3.257459f,5.226847f,7.196236f,9.165625f,11.135007f,13.104395f,15.073785f,17.043201f,19.010683f,20.967133f,22.90179f,24.803998f,26.663291f,28.469434f,30.212487f,31.882853f,33.471341f,34.969206f,36.368205f,37.660639f,38.839393f,39.89797f,40.830557f,41.632015f,42.297934f,42.824648f,43.209258f,43.449647f,43.544491f,43.546122f,43.546122f,43.546122f,43.546122f,43.546122f,43.546122f,43.546122f,43.546122f,43.546122f,43.546122f,43.546122f,43.546122f,43.546122f,43.546122f,43.546122f,43.546122f,43.546122f,43.517475f,43.353121f,43.043755f,42.591077f,41.997583f,41.266535f,40.401961f,39.408614f,38.29197f,37.058167f,35.714f,34.266865f,32.724726f,31.096084f,29.389888f,27.615532f,25.782794f,23.901746f,21.982749f,20.036363f,18.073303f,16.104202f,14.134812f,12.165422f,10.196032f,8.226642f,6.257252f,4.287862f,2.318472f,0.349082f,-1.546191f,-3.427989f,-5.309788f,-7.191587f,-9.073385f,-10.955184f,-12.836982f,-14.718781f,-16.60058f,-18.49292f,-20.37113f,-22.223242f,-24.040787f,-25.815887f,-27.540994f,-29.208984f,-30.813162f,-32.347102f,-33.804695f,-35.180118f,-36.467761f,-37.66224f,-38.75837f,-39.751136f,-40.635743f,-41.407575f,-42.062166f,-42.595434f,-43.00349f,-43.282631f,-43.42993f,-43.453878f,-43.453878f,-43.453878f,-43.453878f,-43.453878f,-43.453878f,-43.453878f,-43.453878f,-43.453878f,-43.258771f,-41.571347f,-40.453878f
    };

    private SplineInterpolator railpath; //spline of our outline

    public int CapCount = 100; //number of caps for guests

    //object inputs
    public GameObject Cap; 
    public GameObject ctlptraw;
    private List<GameObject> pathpoints = new List<GameObject>();                                   //list of points in the path

    private Dictionary<int, float> cappos = new Dictionary<int, float>();                           //tells the parameter of each point
    private List<GameObject> caplist = new List<GameObject>();                                      //list of caps on the rail
    private Dictionary<GameObject, int> tempcaps = new Dictionary<GameObject, int>();               //temporary list of caps
    private List<Vector3> _positions = new List<Vector3>();                                         //list of cap positions
    private Dictionary<GameObject, Guest> _capRiders = new Dictionary<GameObject, Guest>();         //links caps to riders
    private Dictionary<Guest, Vector3> _guests = new Dictionary<Guest, Vector3>();                  //list of all guests and final destination position
    private List<Guest> _riders = new List<Guest>();                                                //list of all guests riding the conveyance


    private Destination[] _destinations;

    public override void SetDestination()
    {
        _destinations = GetComponentsInChildren<Destination>();             //builds a list of access points

        //create the positions dictionary
        for (int i = 0; i < CapCount; i++)
        {
            tempcaps.Add(caplist[i].transform.GetChild(i).gameObject, i);   // replaced with caplist
            _positions.Add(caplist[i].transform.position);
            _capRiders.Add(caplist[i], null);
        }

        //set the occupnacy limit for each waiting lobby
        foreach (Destination destination in _destinations)
        {
            destination.OccupancyLimit = 0;
        }
    }

    private bool SameSign(float x, float y)
    {
        return (x >= 0) ^ (y < 0);
    }

    private SplineInterpolator SetupSpline(List<GameObject> ctlpts)
    {

        SplineInterpolator interp = transform.GetComponent<SplineInterpolator>();

        interp.Reset();
        for (int c = 0; c < ctlpts.Count; c++)
        {
            interp.AddPoint(ctlpts[c].transform.position, ctlpts[c].transform.rotation, c, new Vector2(0, 1));
        }
        interp.StartInterpolation(null, false, eWrapMode.ONCE);
        return interp;
    }

    // Start is called before the first frame update
    void Start()
    {

        //Debug.Log("1");
        _destinations = GetComponentsInChildren<Destination>();

        SplineInterpolator interp = transform.GetComponent<SplineInterpolator>();

        for (int i = 0; i < ptx.Count; i += 1)
        {
            GameObject g = Instantiate(ctlptraw, transform);
            g.transform.localPosition = new Vector3(ptx[i], pty[i], ptz[i]);
            pathpoints.Add(g);
        }

        railpath = SetupSpline(pathpoints);
        int NodeCount = railpath.GetNodeCount();
        //Debug.Log("2");
        for (int i = 0; i < CapCount; i += 1)
        {
            GameObject f = Instantiate(Cap, transform);
            float startpos = i * (NodeCount / CapCount);
            f.transform.position = interp.GetHermiteAtTime(startpos);
            caplist.Add(f);
            cappos.Add(i, startpos);
            _capRiders.Add(f, null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("3");
        for (int i = 0; i < CapCount; i++)
        {
            GameObject car = caplist[i];

            //check if car is open
            if (_capRiders[car] == null)
            {
                //Debug.Log("Car is empty");
                //float carDirection = _positions[i].y - car.transform.position.y;
                //float carDirection = railpath.GetHermiteAtTime(cappos[i - 0]).y - railpath.GetHermiteAtTime(cappos[i]).y;
                //Debug.Log("4");
                foreach (KeyValuePair<Guest, Vector3> kvp in _guests)
                {
                    Guest guest = kvp.Key;

                    //guard statements
                    if (_riders.Contains(guest)) continue; //make sure guest doesn't move between cars
                    if (Vector3.Distance(car.transform.position, guest.transform.position) > 4.1f)
                    {
                        continue;
                    }
                    //Debug.Log("5");
                    //test guest direction
                    float guestDirection = kvp.Value.y - guest.transform.position.y;
                    //if (!SameSign(carDirection, guestDirection)) continue; //continue to next guest
                    //Debug.Log("6");
                    //load guest
                    _riders.Add(guest);
                    _capRiders[car] = guest;
                    //Debug.Log("time to load");
                    IEnumerator coroutine = LoadPassenger(car, guest);
                    StartCoroutine(coroutine);
                    //Debug.Log("check 2");
                    break; //don't check any more guests
                }
            }
            //check if guest has arrived at level
            else
            {
                Guest guest = _capRiders[car];
                Vector3 UnloadPosition = _guests[guest];
                // if (Mathf.Abs(UnloadPosition.y - guest.transform.position.y) < 0.2f)
                //Debug.Log(Vector3.Distance(UnloadPosition, guest.transform.position));
                if (Vector3.Distance(UnloadPosition, guest.transform.position) < 4.1f)
                {
                    //unload guest
                    _capRiders[car] = null;
                    IEnumerator coroutine = UnloadPassenger(car, guest);
                    StartCoroutine(coroutine);
                }
            }
        }
        
        //slide caps along track
        for (int j = 0; j < CapCount; j += 1)
        {
            cappos[j] += Time.deltaTime * Speed;
            Vector3 position = railpath.GetHermiteAtTime(cappos[j]);
            Vector3 forward = position - caplist[j].transform.position;
            caplist[j].transform.forward = new Vector3(forward.x,0,forward.z);
            caplist[j].transform.position = position;
            

            int nodeCount = railpath.GetNodeCount(); //+++
            if (nodeCount % 2 != 0) { nodeCount--; } //+++
            if (cappos[j] >= nodeCount)
            {
                cappos[j] -= nodeCount;
            }
        }
    }

    private IEnumerator LoadPassenger(GameObject car, Guest guest)
    {
        guest.SocialVal += 20f;

        //Debug.Log("passenger loading!");
        bool loading = true;
        while (loading)
        {
            guest.transform.position = Vector3.MoveTowards(guest.transform.position, car.transform.position - new Vector3 (0f,1.75f,0f) + (0.0f * car.transform.forward), Time.deltaTime * Speed * 20);

            if (Vector3.Distance(guest.transform.position, car.transform.position - new Vector3 (0f, 1.75f, 0f) + (0.0f * car.transform.forward)) < 0.01f) { loading = false; }
            yield return new WaitForEndOfFrame();
        }

        guest.transform.forward = car.transform.forward;
        guest.transform.parent = car.transform;
        yield break;
    }

    private IEnumerator UnloadPassenger(GameObject car, Guest guest)
    {
        guest.SocialVal += 20f;
        
        bool unloading = true;
        while (unloading)
        {
            guest.transform.position = Vector3.MoveTowards(guest.transform.position, _guests[guest], Time.deltaTime * Speed * 8);

            if (Vector3.Distance(guest.transform.position, _guests[guest]) < 0.01f) { unloading = false; }
            yield return new WaitForEndOfFrame();
        }

        _riders.Remove(guest);
        _guests.Remove(guest);
        guest.transform.parent = null;
        guest.NextDestination();
        yield break;
    }

    public override void EjectGuest(Guest guest)
    {
        _riders.Remove(guest);
        _guests.Remove(guest);
        guest.transform.parent = null;
        for (int i = 0; i < CapCount; i += 1)
        {
            GameObject c = caplist[i];

            if (_capRiders[c] == guest)
            {
                _capRiders[c] = null;
            }
        }
    }

    public override void ConveyanceUpdate(Guest guest)
    {
        //guard statement if guest is already added
        if (_guests.ContainsKey(guest)) return;

        Destination destination = guest.GetUltimateDestination();
        destination = GetDestination(guest.GetComponent<NavMeshAgent>() , destination.transform.position);
        _guests.Add(guest, destination.transform.position);
        //Debug.Log(destnation.name);
    }

    public override Destination GetDestination(NavMeshAgent agent, Vector3 vec)
    {
        Destination[] tempDestinations = _destinations;
        Destination chosen = _destinations[0];
        List <NavMeshPath> tempnavpaths = new List<NavMeshPath>();
        float destdist = Mathf.Infinity;
        //NavMeshPath temppath;
        //temppath = new NavMeshPath();
        
        foreach (Destination d in tempDestinations)
        {

            GameObject go = Instantiate(GuestManager.Instance.GuestPrefab, GuestManager.Instance.transform.position, agent.transform.rotation);
            //GameObject go = agent.gameObject;
            NavMeshAgent a = go.GetComponent<NavMeshAgent>();
            Guest g = go.GetComponent<Guest>();
            a.enabled = false;
            g.Destination = agent.GetComponent<Guest>().Destination;
            a.enabled = true;
            a.SetDestination(vec);
            float temppathlength = Guest.AgentWalkDistance(a , a.transform, d.transform.position, vec, Color.black);
            Destroy(go);

            //Debug.Log("temppathlenth: " + temppathlength);
            //Debug.Break();
            
            if (temppathlength < destdist)
            {
                //Debug.Log("UpdatedDest");
                //Debug.DrawLine(vec, d.transform.position);
                destdist = temppathlength;
                chosen = d;
                //Debug.Break();
            }
        }
        
        //Debug.Log("chosen: " + chosen);
        //Debug.Log("destdist: " + destdist);
        //Debug.DrawLine(chosen.transform.position, vec);
        //Debug.Break();
        return chosen;
    }

    public override Vector3 StartPosition(NavMeshAgent agent, Vector3 vec)
    {
        if (_destinations.Length == 0) { Debug.Log("NoConveyanceDest"); return Vector3.zero; }
        Destination destination = GetDestination(agent, vec);
        //Debug.Log(destination.name);
        return destination.transform.position;
    }

    public override Vector3 EndPosition(NavMeshAgent agent, Vector3 vec)
    {
        if (_destinations.Length == 0) { Debug.Log("NoConveyanceDest"); return Vector3.zero; }
        Destination destination = GetDestination(agent, vec);
        return destination.transform.position;
    }

    public override float WeightedTravelDistance(NavMeshAgent agent, Vector3 start, Vector3 end)
    {
        float distance = 0;
        //guard statement
        if (_destinations.Length < 2) return distance;

        //get the total path distance
        Destination go1 = GetDestination(agent, start);
        Destination go2 = GetDestination(agent, end);
        distance = Vector3.Distance(go1.transform.position, go2.transform.position);

        //we scale the distance by the weight factor
        distance /= Weight;
        return distance;
    }
    public static float GetPathLength(NavMeshPath path)
    {
        float lng = 0.0f;
        for (int i = 1; i < path.corners.Length; ++i)
        {
            lng += Vector3.Distance(path.corners[i - 1], path.corners[i]);
        }

        return lng;
    }


    /*/public static float NavMeshLength(Vector3 start, Vector3 end)
    {
        NavMeshPath path = new NavMeshPath();
       
        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

        allWayPoints[0] = start;

        // The last point is the target position.
        allWayPoints[allWayPoints.Length - 1] = end;

        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        float pathLength = 0;

        for (int i = 0; i < allWayPoints.Length - 1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }
        return pathLength;
    }/*/

    /*/public static float NavMeshDistance(NavMeshAgent agent, Transform trans, Vector3 start, Vector3 end, Color color)
    {
        //in case they are the same position
        if (Vector3.Distance(start, end) < 0.01f) { Debug.Log("SmallDistanceError"); return 0; }

        //move agent to the start position
        Vector3 initialPosition = trans.position;           //remembers where he was
        agent.enabled = false;
        trans.position = start;//_agent.Move(start - initialPosition);
        agent.enabled = true;

        //test to see if agent has path or not
        float distance = Mathf.Infinity;
        NavMeshPath navMeshPath = agent.path;
        if (!agent.CalculatePath(end, navMeshPath))
        {
            //reset agent to original position
            agent.enabled = false;
            trans.position = initialPosition;//_agent.Move(initialPosition - start);
            agent.enabled = true;
            //Debug.Log("Infinity1: " + distance);
            return distance;
        }

        //check to see if there is a path
        Vector3[] path = navMeshPath.corners;
        if (path.Length < 2 || Vector3.Distance(path[path.Length - 1], end) > 2) //2
        {
            //reset agent to original position
            agent.enabled = false;
            trans.position = initialPosition;//_agent.Move(initialPosition - start);
            agent.enabled = true;
            //Debug.Log("Infinity2: " + distance);
            return distance;
        }

        //get walking path distance
        distance = 0;
        for (int i = 1; i < path.Length; i++)
        {
            distance += Vector3.Distance(path[i - 1], path[i]);
            Debug.DrawLine(path[i - 1], path[i], color); //visualizing the path, not necessary to return

        }

        //reset agent to original position
        agent.enabled = false;
        trans.position = initialPosition;//_agent.Move(initialPosition - start);
        agent.enabled = true;

        return distance;
    }/*/
}
