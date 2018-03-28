using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FullBar.Core;
using System.Data;

namespace FullBar.Repositories
{
    public class UsuarioRepository : BaseRepository
    {
        public UsuarioRepository() : base(new DB_TESTEEntities()) { 
        
        }

        public Int64 SaveOrUpdate(TB_Aluno _model)
        {
            var message = string.Empty;

            try
            {
                var conteudo = base.Context.TB_Aluno
                    .FirstOrDefault(p => p.idAluno == _model.idAluno);
                if (conteudo == null)
                {
                    conteudo = new TB_Aluno();
                }
                conteudo.idAluno = _model.idAluno;
                conteudo.Nome = _model.Nome;
                conteudo.RA = _model.RA;
                conteudo.idCurso = _model.idCurso;
                conteudo.idPeriodo = _model.idPeriodo;

                if (_model.IsNew)
                {
                    conteudo.Nome = _model.Nome;
                    conteudo.RA = _model.RA;
                    conteudo.idCurso = _model.idCurso;
                    conteudo.idPeriodo = _model.idPeriodo;
                    base.Context.TB_Aluno.Add(conteudo);
                    base.Context.Entry(conteudo).State = EntityState.Added;
                }
                else
                {
                    
                    base.Context.Entry(conteudo).State = EntityState.Modified;
                }

                base.Context.SaveChanges();
                return conteudo.idAluno;
            }
            catch (Exception ex)
            {
                message = ex.Message.ToString();
                return 0;
            }
        }

        public IList<TB_Curso> ListaCurso()
        {
            return base.Context.TB_Curso.ToList();
        }

        public IList<TB_Periodo> ListaPeriodo()
        {
            return base.Context.TB_Periodo.ToList();
        }

        public IList<TB_Aluno> ListaAlunos()
        {
            return base.Context.TB_Aluno.ToList();
        }

        public List<TB_Aluno> ListaAlunosFiltros(TB_Aluno _model)
        {

            return  base.Context.TB_Aluno.Where(
                    p => (p.idCurso == _model.idCurso || _model.idCurso == 0)
                   && (p.Nome.Contains(_model.Nome) ||  _model.Nome == "")
                   && (p.RA.Contains(_model.RA) || _model.RA == "")).ToList();

              

        
        }

        public void DeletarAluno(int idaluno) {
            var conteudo = base.Context.TB_Aluno
                    .FirstOrDefault(p => p.idAluno == idaluno);
            base.Context.TB_Aluno.Remove(conteudo);
            base.Context.SaveChanges();
            
        }
    }
}