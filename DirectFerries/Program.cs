using DirectFerries.Domain;

const string baseUrl = "https://dummyjson.com";

var login = new Login(baseUrl);
var client = await login.AuthenticateAsync();

if (client != null)
{
    var product = new ProductService(client, baseUrl);
    await product.ProcessProductsAsync();
}
