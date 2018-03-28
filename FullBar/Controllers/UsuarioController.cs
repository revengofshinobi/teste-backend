using FullBar.Core;
using FullBar.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace FullBar.Controllers
{
    public class UsuarioController : Controller
    {
        //
        // GET: /Usuario/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AlterarUsuario(string nomealuno, string raluno, string tbcurso)
        {
            var _repoUsuario = new UsuarioRepository();
            UsuarioFiltros uf = new UsuarioFiltros();
            uf.tbcurso = _repoUsuario.ListaCurso();
            uf.tbperiodo = _repoUsuario.ListaPeriodo();

            if (!String.IsNullOrEmpty(nomealuno) || !String.IsNullOrEmpty(raluno) || !String.IsNullOrEmpty(tbcurso))
            { 
                TB_Aluno ta = new TB_Aluno();
                ta.Nome = nomealuno;
                ta.RA = raluno;
                ta.idCurso = Convert.ToInt32(tbcurso);
                uf.tbaluno  = _repoUsuario.ListaAlunosFiltros(ta);
            }
            else { uf.tbaluno = _repoUsuario.ListaAlunos();  }
            
            return View(uf);
        }

        

        public ActionResult CadastroUsuario()
        {
            var _repoUsuario = new UsuarioRepository();
            UsuarioFiltros uf = new UsuarioFiltros();
            uf.tbcurso = _repoUsuario.ListaCurso();
            uf.tbperiodo = _repoUsuario.ListaPeriodo();
            return View(uf);
             
        }
        [HttpPost]
        public JsonResult Cadastrar(string nomealuno, string raluno,
                string idcurso, string idperiodo)
        {

            var _repoUsuario = new UsuarioRepository();
            var IsSucesso = false;
            var IsErro = false;
            string message = "";

            if (String.IsNullOrEmpty(nomealuno)) { IsErro = true; IsSucesso = false; message += "Nome Aluno em branco !<br>"; }
            if (String.IsNullOrEmpty(raluno)) { IsErro = true; IsSucesso = false; message += "RA Aluno em branco !<br>"; }
            if (String.IsNullOrEmpty(idcurso)) { IsErro = true; IsSucesso = false; message += "Selecione um curso !<br>"; }
            if (String.IsNullOrEmpty(idperiodo)) { IsErro = true; IsSucesso = false; message += "Selecione um periodo !<br>"; }
            

            //Colocar o try parse
            try{
               int val1 = Convert.ToInt32(idcurso);
               int val = Convert.ToInt32(idperiodo);
               if (val1 <= 0 || val <= 0) {
                   IsErro = true;
                   IsSucesso = false;
                   message += "Selecione um valor correto";
               }

            }catch{
                IsErro = true;
                IsSucesso = false;
                message +="Selecione um valor correto";
            }
            if (IsErro != true)
            {
                TB_Aluno model = new TB_Aluno();
                model.Nome = nomealuno;
                model.RA = raluno;
                model.idCurso = Convert.ToInt32(idcurso);
                model.idPeriodo = Convert.ToInt32(idperiodo);

                if (_repoUsuario.SaveOrUpdate(model) > 0)
                {
                    IsSucesso = true;
                    IsErro = false;
                    message = "Aluno(A) Cadastrado com sucesso !";
                }
            }

            return Json(new
            {

                IsSucesso = IsSucesso,
                IsErro = IsErro,
                Message = message
            });
        }

        [HttpPost]
        public JsonResult AtualizarAluno(int idaluno ,string nomealuno, string raluno,
                string idcurso, string idperiodo)
        {

            var _repoUsuario = new UsuarioRepository();
            var IsSucesso = false;
            var IsErro = false;
            string message = "";

            if (String.IsNullOrEmpty(nomealuno)) { IsErro = true; IsSucesso = false; message += "Nome Aluno em branco !<br>"; }
            if (String.IsNullOrEmpty(raluno)) { IsErro = true; IsSucesso = false; message += "RA Aluno em branco !<br>"; }
            if (String.IsNullOrEmpty(idcurso)) { IsErro = true; IsSucesso = false; message += "Selecione um curso !<br>"; }
            if (String.IsNullOrEmpty(idperiodo)) { IsErro = true; IsSucesso = false; message += "Selecione um periodo !<br>"; }


            //Colocar o try parse
            try
            {
                int val1 = Convert.ToInt32(idcurso);
                int val = Convert.ToInt32(idperiodo);
                if (val1 <= 0 || val <= 0)
                {
                    IsErro = true;
                    IsSucesso = false;
                    message += "Selecione um valor correto";
                }

            }
            catch
            {
                IsErro = true;
                IsSucesso = false;
                message += "Selecione um valor correto";
            }
            if (IsErro != true)
            {
                TB_Aluno model = new TB_Aluno();
                model.idAluno = idaluno;
                model.Nome = nomealuno;
                model.RA = raluno;
                model.idCurso = Convert.ToInt32(idcurso);
                model.idPeriodo = Convert.ToInt32(idperiodo);

                if (_repoUsuario.SaveOrUpdate(model) > 0)
                {
                    IsSucesso = true;
                    IsErro = false;
                    message = "Aluno(A) Atualizado com sucesso !";
                }
            }

            return Json(new
            {

                IsSucesso = IsSucesso,
                IsErro = IsErro,
                Message = message
            });
        }

        [HttpPost]
        public JsonResult DeletarAluno(int idaluno)
        {

            var _repoUsuario = new UsuarioRepository();
            var IsSucesso = false;
            var IsErro = false;
            string message = "";

           
            //Colocar o try parse
            try
            {
                _repoUsuario.DeletarAluno(idaluno);
                
                    IsSucesso = true;
                    IsErro = false;
                    message = "Aluno(A) Deletado com sucesso !";
                

            }
            catch
            {
                IsErro = true;
                IsSucesso = false;
                message += "Erro";
            }
          

            return Json(new
            {

                IsSucesso = IsSucesso,
                IsErro = IsErro,
                Message = message
            });
        }


        
    }
}
