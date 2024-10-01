namespace web_api.helpers;

public class NumeroLegajo
{
    //Atributo de clase.
    private static NumeroLegajo? instance = null;

    private NumeroLegajo()
    {
        this.Numero = 1;
    }

    //Método de clase.
    public static NumeroLegajo? GetInstance()
    {
        if(instance == null)
        {
            instance = new NumeroLegajo();
        }

        return instance;
    }

    private long Numero { get; set; } = 0;

    public long GetNextNumber()
    {
        this.Numero++;
        return this.Numero;
    }
}