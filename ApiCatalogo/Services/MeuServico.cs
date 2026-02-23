namespace ApiCatalogo.Services
{
    public class MeuServico : IMeuServico
    {
        public string Saudacao(string nome)
        {
            return $"Olá, {nome}! Bem-vindo ao meu serviço. \n \n {DateTime.UtcNow}";
        }
    }
}
