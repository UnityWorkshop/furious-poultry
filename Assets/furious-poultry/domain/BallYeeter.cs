namespace com.github.UnityWorkshop.furious_poultry.domain
{
    public class BallYeeter
    {
        public int SensX;
        public int SensY;
        
        public float ForceValue;
        private float _xRotation;
        private float _yRotation;

        public BallYeeter(float forceValue)
        {
            ForceValue = forceValue;
        }
    }
}