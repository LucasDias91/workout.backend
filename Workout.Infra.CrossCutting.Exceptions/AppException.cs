using System.Globalization;
namespace Workout.Infra.CrossCutting.Exceptions
{
    public class AppException : Exception
    {
        public AppException() : base() { }

        public AppException(string message) : base(message) { }

        public AppException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args)) { }
    }

    public class InvalidCredentialException : AppException
    {
        public InvalidCredentialException() : base(String.Format("Credências inválidas")) { }
    }

    public class AccountPeddingException : AppException
    {
        public AccountPeddingException() : base(String.Format("Conta pendente de ativação. Entre no seu e-mail e finalize a ativação")) { }
    }

    public class AccountActiveException : AppException
    {
        public AccountActiveException() : base(String.Format("Essa conta já cadastrada! Caso tenha esquecido sua senha, clique em 'Esqueci minha senha!'")) { }
    }
    public class AccountInactiveException : AppException
    {
        public AccountInactiveException() : base(String.Format("Essa conta está inativa! Entre em contato com o administrador")) { }
    }

    public class CompanyNotFoundException : AppException
    {
        public CompanyNotFoundException() : base(String.Format("Empresa não encontrada.")) { }
    }
    public class ApplicationNotFoundException : AppException
    {
        public ApplicationNotFoundException() : base(String.Format("Nenhuma aplicação foi encontrada.")) { }
    }
    public class EmailExistsException : AppException
    {
        public EmailExistsException() : base(String.Format("Email já cadastrado.")) { }
    }
    public class UserNotFoundException : AppException
    {
        public UserNotFoundException() : base(String.Format("Usuário não encontrado.")) { }
    }
    public class SubjectNotFoundException : AppException
    {
        public SubjectNotFoundException() : base(String.Format("Assunto não encontrado.")) { }
    }
    public class CustmertNotFoundException : AppException
    {
        public CustmertNotFoundException() : base(String.Format("Cliente não encontrado.")) { }
    }

    public class ActivationtNotFoundException : AppException
    {
        public ActivationtNotFoundException() : base(String.Format("Código de ativação não encontrado.")) { }
    }

    public class UnauthorizedException : AppException
    {
        public UnauthorizedException() : base(String.Format("Usuário ou senha inválidos!")) { }
        public UnauthorizedException(string message) : base(message) { }
    }
    public class ForbiddenException : AppException
    {
        public ForbiddenException() : base(String.Format("Conta pendende de ativação.")) { }
        public ForbiddenException(string message) : base(message) { }
    }
    public class RefreshTokenException : UnauthorizedException
    {
        public RefreshTokenException() : base(String.Format("Token inválido ou expirado!")) { }
    }

    public class ConfigurationNotFoundException : AppException
    {
        public ConfigurationNotFoundException() : base(String.Format("Configuração não encontrada.")) { }
    }

    public class ContactNotFoundException : AppException
    {
        public ContactNotFoundException() : base(String.Format("Contato não encontrado.")) { }
    }

    public class AddressNotFoundException : AppException
    {
        public AddressNotFoundException() : base(String.Format("Endereço não encontrado.")) { }
    }

    public class PageTokenNotFoundException : AppException
    {
        public PageTokenNotFoundException() : base(String.Format("Link inválido ou expidado.")) { }
    }
}
