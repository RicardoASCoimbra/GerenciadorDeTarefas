using Microsoft.AspNetCore.Mvc;
using Moq;
using Tarefas.Domain.Enuns;
using Tarefas.Services.Interfaces.Tarefa;
using Tarefas.Services.ViewModels.Tarefa;
using Tarefas.Web.Controllers.Tarefa;

namespace Tarefas.Tests
{
    [TestFixture]
    public class TarefaControllerTests
    {
        private TarefaController_TesteController _controller;
        private Mock<ITarefaAppService> _tarefaAppService;

        [SetUp]
        public void Setup()
        {
            _tarefaAppService = new Mock<ITarefaAppService>();
            _controller = new TarefaController_TesteController(_tarefaAppService.Object);
        }

        [Test]
        public async Task Post_Creates_New_Tarefa_With_Valid_Model()
        {
            // Arrange
            var tarefaViewModel = new TarefaViewModel
            {
                Nome = "Nova Tarefa",
                Descricao = "Descrição da nova tarefa",
                Status = Status.A_fazer,
                Prioridades = Prioridades.Média,
                DataCadastro = DateTime.Now,
                DataInicio = DateTime.Now,
                DataFim = DateTime.Now.AddMonths(3),
                Responsavel = "Ricardo Coimbra"
            };

            // Configure o seu serviço mock
            _tarefaAppService.Setup(s => s.Create(It.IsAny<TarefaViewModel>()))
                .ReturnsAsync(tarefaViewModel);

            // Act
            var result = await _controller.Post(tarefaViewModel);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<CreatedAtActionResult>(result);
            var createdResult = (CreatedAtActionResult)result;
            Assert.IsInstanceOf<TarefaViewModel>(createdResult.Value);
            var createdTarefa = (TarefaViewModel)createdResult.Value;
            Assert.Multiple(() =>
            {
                Assert.That(createdTarefa.Nome, Is.EqualTo(tarefaViewModel.Nome));
                Assert.That(createdTarefa.Descricao, Is.EqualTo(tarefaViewModel.Descricao));
                Assert.That(createdTarefa.Status, Is.EqualTo(tarefaViewModel.Status));
                Assert.That(createdTarefa.Prioridades, Is.EqualTo(tarefaViewModel.Prioridades));
                Assert.That(createdTarefa.DataCadastro, Is.EqualTo(tarefaViewModel.DataCadastro));
                Assert.That(createdTarefa.DataInicio, Is.EqualTo(tarefaViewModel.DataInicio));
                Assert.That(createdTarefa.DataFim, Is.EqualTo(tarefaViewModel.DataFim));
                Assert.That(createdTarefa.Responsavel, Is.EqualTo(tarefaViewModel.Responsavel));
            });

            // Verifique se o método Create foi chamado no serviço
            _tarefaAppService.Verify(s => s.Create(It.IsAny<TarefaViewModel>()), Times.Once);
        }

        [Test]
        public async Task Get_Returns_Tarefa_With_Valid_Id()
        {
            // Arrange
            var id = Guid.NewGuid();
            var tarefa = new TarefaViewModel
            {
                Id = id,
                Nome = "Tarefa de teste"
            };

            // Configure o seu serviço mock
            _tarefaAppService.Setup(s => s.GetById(id)).ReturnsAsync(tarefa);

            // Act
            var result = await _controller.Get(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okObjectResult = (OkObjectResult)result;
            Assert.IsInstanceOf<TarefaViewModel>(okObjectResult.Value);
            var returnedTarefa = (TarefaViewModel)okObjectResult.Value;

            Assert.That(returnedTarefa.Id, Is.EqualTo(id));
            Assert.That(returnedTarefa.Nome, Is.EqualTo(tarefa.Nome));

            // Verifique se o método GetById foi chamado no serviço
            _tarefaAppService.Verify(s => s.GetById(id), Times.Once);
        }

        [Test]
        public async Task GetAll_Returns_List_Of_Tarefas()
        {
            // Arrange
            var tarefas = new List<TarefaViewModel> { new TarefaViewModel(), new TarefaViewModel() };

            // Configure o seu serviço mock
            _tarefaAppService.Setup(s => s.GetAll()).ReturnsAsync(tarefas);

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okObjectResult = (OkObjectResult)result;
            Assert.IsInstanceOf<List<TarefaViewModel>>(okObjectResult.Value);
            var returnedTarefas = (List<TarefaViewModel>)okObjectResult.Value;
            Assert.That(returnedTarefas.Count, Is.EqualTo(tarefas.Count));

            // Verifique se o método GetAll foi chamado no serviço
            _tarefaAppService.Verify(s => s.GetAll(), Times.Once);
        }

        [Test]
        public async Task Put_Updates_Tarefa_With_Valid_Model()
        {
            // Arrange
            var tarefaViewModel = new TarefaViewModel
            {
                Id = Guid.NewGuid(),
                Nome = "Tarefa Atualizada",
                Descricao = "Descrição Atualizada",
                Status = Status.A_fazer,
                Prioridades = Prioridades.Baixa,
                DataCadastro = DateTime.Now,
                DataInicio = DateTime.Now,
                DataFim = DateTime.Now.AddMonths(3),
                Responsavel = "Ricardo Coimbra"
            };

            // Configure o seu serviço mock
            _tarefaAppService.Setup(s => s.Create(It.IsAny<TarefaViewModel>()))
                .ReturnsAsync((TarefaViewModel tarefaViewModel) => tarefaViewModel);

            // Act
            var result = await _controller.Put(tarefaViewModel);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<NoContentResult>(result);

            // Verifique se o método Update foi chamado no serviço
            _tarefaAppService.Verify(s => s.Update(tarefaViewModel), Times.Once);
        }

        [Test]
        public async Task Delete_Deletes_Tarefa_With_Valid_Id()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Configure o seu serviço mock
            _tarefaAppService.Setup(s => s.Delete(id)).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<NoContentResult>(result);

            // Verifique se o método Delete foi chamado no serviço
            _tarefaAppService.Verify(s => s.Delete(id), Times.Once);
        }

        [Test]
        public async Task GetByFilter_Returns_List_Of_Tarefas_With_Valid_Filter()
        {
            // Arrange
            var nome = "Nome do usuário";
            var responsavel = "Sistema de origem";
            var tarefas = new List<TarefaViewModel> { new TarefaViewModel(), new TarefaViewModel() };

            // Configure o seu serviço mock
            _tarefaAppService.Setup(s => s.GetByFilter(nome, responsavel)).ReturnsAsync(tarefas);

            // Act
            var result = await _controller.GetByFilter(nome, responsavel);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okObjectResult = (OkObjectResult)result;
            Assert.IsInstanceOf<List<TarefaViewModel>>(okObjectResult.Value);
            var returnedTarefas = (List<TarefaViewModel>)okObjectResult.Value;
            Assert.That(returnedTarefas.Count, Is.EqualTo(tarefas.Count));

            // Verifique se o método GetByFilter foi chamado no serviço
            _tarefaAppService.Verify(s => s.GetByFilter(nome, responsavel), Times.Once);
        }
    }
}
