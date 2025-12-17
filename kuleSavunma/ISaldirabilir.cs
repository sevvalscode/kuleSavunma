using System.Collections.Generic;

namespace kuleSavunma
{
    // ISaldirabilir Arabirimi (Interface)
    // Bu arabirim, bir sınıfın saldırı yapabileceğini garanti eder.
    public interface ISaldirabilir
    {
        void Saldir(List<Canavar> canavarlar, List<AtisEfekti> efektler);
    }
}