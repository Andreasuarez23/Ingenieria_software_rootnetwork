namespace web_api.helpers;

public class VisitCounter
{
    //Atributo de clase.
    private static VisitCounter? instante = null;

    private VisitCounter()
    {
        this.Number = 100;
    }

    //Método de clase.
    public static VisitCounter? GetInstance()
    {
        if(instante == null)
        {
            instante = new VisitCounter();
        }

        return instante;
    }

    private long Number { get; set; } = 0;

    public long GetNextNumber()
    {
        this.Number++;
        return this.Number;
    }

    public long GetNumber()
    {
        return this.Number;
    }
}