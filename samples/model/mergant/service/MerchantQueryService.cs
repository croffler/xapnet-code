
using GigaSpaces.Core;

using samples.model;

public class MerchantQueryService
{

    private ISpaceProxy Proxy;

    public MerchantQueryService(ISpaceProxy proxy)
    {
        this.Proxy = proxy;
    }

    // Return a collection of Merchants
    public Merchant[] findAllMerchants()
    {

        Merchant template = new Merchant();
        return Proxy.ReadMultiple<Merchant>(template);
    }

    // Find Merchant by id
    public Merchant findMerchantById(long? id)
    {
        return Proxy.ReadById<Merchant>(id);
    }
}
