using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.Services
{
    public static class VerifyLogin
    {
        public static bool UsuarioLogado(this ISession session)
        {
            if (session.GetString("SessionUser") == null) // Verifico se existe um usuario na minha sessão
            {
                return false;  // Se não existir, redireciono para o index (tela de login)
            }
            return true;
        }
    }
}
