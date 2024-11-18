    using Moq;
    using Xunit;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Marynsino2513.ORM;
    using Marynsino2513.Repositorios;


    namespace SiteAgendamento.Tests
    {
        public class ServicoRepositorioTests
        {
            private readonly Mock<BdMarynsinoContext> _mockContext;
            private readonly ServicoRepositorio _repositorio;
        

        public ServicoRepositorioTests()
            {
                // Criando um Mock para o DbSet de TbUsuario
                _mockContext = new Mock<BdMarynsinoContext>();
                _repositorio = new ServicoRepositorio(_mockContext.Object);
            }

            [Fact]
            public void InserirServico_DeveRetornarTrue_QuandoInserirComSucesso()
            {
                // Arrange
          
                var tipoServico = "Aula de matematica";
                var valor = 1000;
                

                var mockDbSet = new Mock<DbSet<TbServico>>();
                _mockContext.Setup(m => m.TbServicos).Returns(mockDbSet.Object);

           
            var resultado = _repositorio.InserirServico(tipoServico,valor);

                // Assert
                Assert.True(resultado);
                _mockContext.Verify(m => m.SaveChanges(), Times.Once);
            }

            [Fact]
            public void ListarServicos_DeveRetornarListaDeServicos()
            {
                // Arrange
                var mockDbSet = new Mock<DbSet<TbServico>>();
                var servicos = new List<TbServico>
            {
                new TbServico { Id = 1, TipoServico = "Aula online", Valor = 100 },
                new TbServico { Id = 2, TipoServico = "Aula de matematica", Valor = 200 }
            }.AsQueryable();

                mockDbSet.As<IQueryable<TbServico>>().Setup(m => m.Provider).Returns(servicos.Provider);
                mockDbSet.As<IQueryable<TbServico>>().Setup(m => m.Expression).Returns(servicos.Expression);
                mockDbSet.As<IQueryable<TbServico>>().Setup(m => m.ElementType).Returns(servicos.ElementType);
                mockDbSet.As<IQueryable<TbServico>>().Setup(m => m.GetEnumerator()).Returns(servicos.GetEnumerator());

                _mockContext.Setup(m => m.TbServicos).Returns(mockDbSet.Object);

                // Act
                var resultado = _repositorio.ListarServicos();

                // Assert
                Assert.Equal(2, resultado.Count);
                Assert.Equal("Aula online", resultado[0].TipoServico);
                Assert.Equal("Aula de matematica", resultado[1].TipoServico);
            }

            [Fact]
            public void AtualizarServico_DeveRetornarTrue_QuandoAtualizacaoForBemSucedida()
            {
                // Arrange
                var id = 1;
                var tipoServico = "Aula online";
                var valor = 100;

                // Criando o mock do DbSet como IQueryable
                var servico = new TbServico { Id = id, TipoServico = "Aula online", Valor = 100 };
                var listaServicos = new List<TbServico> { servico }.AsQueryable();

                var mockDbSet = new Mock<DbSet<TbServico>>();

                // Configurando o mock do DbSet para se comportar como IQueryable
                mockDbSet.As<IQueryable<TbServico>>()
                         .Setup(m => m.Provider).Returns(listaServicos.Provider);
                mockDbSet.As<IQueryable<TbServico>>()
                         .Setup(m => m.Expression).Returns(listaServicos.Expression);
                mockDbSet.As<IQueryable<TbServico>>()
                         .Setup(m => m.ElementType).Returns(listaServicos.ElementType);
                mockDbSet.As<IQueryable<TbServico>>()
                         .Setup(m => m.GetEnumerator()).Returns(listaServicos.GetEnumerator());

                // Configurando o mock do contexto
                _mockContext.Setup(m => m.TbServicos).Returns(mockDbSet.Object);

                // Act
                var resultado = _repositorio.AtualizarServico(id, tipoServico, valor);

                // Assert
                Assert.True(resultado);
                Assert.Equal(tipoServico, servico.TipoServico);
                Assert.Equal(valor, servico.Valor);
                

                // Verificando que SaveChanges foi chamado uma vez
                _mockContext.Verify(m => m.SaveChanges(), Times.Once);
            }


            [Fact]
            public void ExcluirServico_DeveRetornarTrue_QuandoExcluirComSucesso()
            {
                // Arrange
                var id = 12;
                var servico = new TbServico
                {
                    Id = id,
                    TipoServico = "Aula de matematica",
                    Valor = 100
                };

                // Lista de usuários simulada
                var servicos = new List<TbServico>
    {
        new TbServico { Id = 1, TipoServico = "Aula de matematica", Valor = 100 },
        new TbServico { Id = 2, TipoServico = "Aula de online", Valor = 200 },
        servico // Usuário que será excluído
    }.AsQueryable(); // Converte para IQueryable

                // Mock do DbSet<TbUsuario>
                var mockDbSet = new Mock<DbSet<TbServico>>();

                // Configura o mock do DbSet para retornar o IQueryable
                mockDbSet.As<IQueryable<TbServico>>()
                         .Setup(m => m.Provider).Returns(servicos.Provider);
                mockDbSet.As<IQueryable<TbServico>>()
                         .Setup(m => m.Expression).Returns(servicos.Expression);
                mockDbSet.As<IQueryable<TbServico>>()
                         .Setup(m => m.ElementType).Returns(servicos.ElementType);
                mockDbSet.As<IQueryable<TbServico>>()
                         .Setup(m => m.GetEnumerator()).Returns(servicos.GetEnumerator());

                // Configura o contexto para retornar o mock do DbSet
                _mockContext.Setup(m => m.TbServicos).Returns(mockDbSet.Object);

                // Act
                var resultado = _repositorio.ExcluirServico(id);

                // Assert
                Assert.True(resultado);
                _mockContext.Verify(m => m.SaveChanges(), Times.Once);  // Verifica que SaveChanges foi chamado
            }

        }
    }

