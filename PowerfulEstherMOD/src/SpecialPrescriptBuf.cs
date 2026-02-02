using System.Collections.Generic;

public class SpecialPrescriptBuf : PrescriptBuf
{
    public virtual bool ShouldSendScript()
    {
        return false;
    }

    public virtual List<LorId> InitializeHand()
    {
        return new List<LorId>();
    }
}
