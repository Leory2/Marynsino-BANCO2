﻿using Marynsino2513.Models;
using Marynsino2513.ORM;

namespace Marynsino2513.Repositorios
{
    public class ServicoRepositorio
    {
        private BdMarynsinoContext _context;
        public ServicoRepositorio(BdMarynsinoContext context)
        {
            _context = context;
        }
        public List<ServicoVM> ListarServicos()
        {
            List<ServicoVM> listSer = new List<ServicoVM>();

            var listTb = _context.TbServicos.ToList();

            foreach (var item in listTb)
            {
                var servicos = new ServicoVM
                {
                    Id = item.Id,
                    TipoServico = item.TipoServico,
                    Valor = item.Valor,
                };

                listSer.Add(servicos);
            }

            return listSer;
        }

        public bool InserirServico(string tipoServico, decimal valor)
        {
            try
            {
                TbServico servico = new TbServico();
                servico.TipoServico = tipoServico;
                servico.Valor = valor;
                    
                  _context.TbServicos.Add(servico);  // Supondo que _context.TbUsuarios seja o DbSet para a entidade de usuários
                _context.SaveChanges();

                return true;  // Retorna true para indicar sucesso
            }
            catch (Exception ex)
            {
                // Trate o erro ou faça um log do ex.Message se necessário
                return false;  // Retorna false para indicar falha
            }
        }
        public List<ServicoVM> ServicoUsuarios()
        {
            List<ServicoVM> listSer = new List<ServicoVM>();

            var listTb = _context.TbServicos.ToList();

            foreach (var item in listTb)
            {
                var servicos = new ServicoVM
                {
                    Id = item.Id,
                    TipoServico = item.TipoServico,
                };

                listSer.Add(servicos);
            }

            return listSer;
        }
        public bool AtualizarServico(int id, string tipoServico, decimal valor)
        {
            try
            {
                // Busca o usuário pelo ID
                var servico = _context.TbServicos.FirstOrDefault(u => u.Id == id);
                if (servico != null)
                {
                    // Atualiza os dados do usuário
                    servico.TipoServico = tipoServico;
                    servico.Valor= valor;

                    // Salva as mudanças no banco de dados
                    _context.SaveChanges();

                    return true;  // Retorna verdadeiro se a atualização for bem-sucedida
                }
                else
                {
                    return false;  // Retorna falso se o usuário não foi encontrado
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar o serviço com ID {id}: {ex.Message}");
                return false;
            }
        }
        public bool ExcluirServico(int id)
        {
            try
            {
                // Busca o usuário pelo ID
                var servico = _context.TbServicos.FirstOrDefault(u => u.Id == id);

                // Se o usuário não for encontrado, lança uma exceção personalizada
                if (servico == null)
                {
                    throw new KeyNotFoundException("Usuário não encontrado.");
                }


                // Remove o usuário do banco de dados
                _context.TbServicos.Remove(servico);
                _context.SaveChanges();  // Isso pode lançar uma exceção se houver dependências

                // Se tudo correr bem, retorna true indicando sucesso
                return true;

            }
            catch (Exception ex)
            {
                // Aqui tratamos qualquer erro inesperado e logamos para depuração
                Console.WriteLine($"Erro ao excluir o usuário com ID {id}: {ex.Message}");

                // Relança a exceção para ser capturada pelo controlador
                throw new Exception($"Erro ao excluir o usuário: {ex.Message}");
            }
        }
    }
}