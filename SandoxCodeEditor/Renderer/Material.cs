
namespace SandboxCodeEditor.Renderer
{
    public class Material
    {
        public string name = "none";
        public Pixel main_Color = Pixel.White;
        public Texture texture = null;

        public static Material Default 
        { 
            get 
            { 
                Material def_material = new Material(); 
                def_material.name = "Default"; 
                return def_material; 
            } 
        }
    }
}
