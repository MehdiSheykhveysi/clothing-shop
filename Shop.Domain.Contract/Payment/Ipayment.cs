using Shop.Domain.Entities;

namespace Shop.Domain.Contract
{
    public interface IPayment
    {
        PaymentRequest Pay(PayInput input);
        VerifyResponse Verify(string Authority);
    }
}