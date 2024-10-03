namespace web_api.helpers;

public class Configuracion
{
    //Atributo de clase.
    private static Configuracion? instance = null;

    private Configuracion()
    {
        
    }

    //Método de clase.
    public static Configuracion? GetInstance()
    {
        if(instance == null)
        {
            instance = new Configuracion();
        }

        return instance;
    }

    public string Mascara { get; set; } = "";
}