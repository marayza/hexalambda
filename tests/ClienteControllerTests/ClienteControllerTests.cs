using hexalambda.Controllers;
using hexalambda.Models;
using hexalambda.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;


[TestFixture]
public class ClienteControllerTests
{
    [Test]
    public async Task AutenticarPorCPF_ExistingCliente_ReturnsOkResult()
    {
        string cpf = "12345678900"; // Substitua pelo CPF desejado
        var clienteRepositoryMock = new Mock<IClienteRepository>();
        clienteRepositoryMock.Setup(repo => repo.GetByCPFAsync(cpf)).ReturnsAsync(new Cliente()); // Substitua com os dados de um cliente existente

        var controller = new ClienteController(clienteRepositoryMock.Object);
        var result = await controller.AutenticarPorCPF(cpf);

        Assert.IsInstanceOf<OkObjectResult>(result);
        var okResult = (OkObjectResult)result;
        Assert.IsInstanceOf<Cliente>(okResult.Value);
    }

    [Test]
    public async Task AutenticarPorCPF_NonExistingCliente_ReturnsNotFoundResult()
    {
        string cpf = "32621";
        var clienteRepositoryMock = new Mock<IClienteRepository>();
        clienteRepositoryMock.Setup(repo => repo.GetByCPFAsync(cpf)).ReturnsAsync((Cliente)null);

        var controller = new ClienteController(clienteRepositoryMock.Object);
        var result = await controller.AutenticarPorCPF(cpf);

        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task AutenticarPorCPF_Exception_ReturnsInternalServerError()
    {
        string cpf = "3369969451";
        var clienteRepositoryMock = new Mock<IClienteRepository>();
        clienteRepositoryMock.Setup(repo => repo.GetByCPFAsync(cpf)).ThrowsAsync(new Exception("Teste exception"));

        var controller = new ClienteController(clienteRepositoryMock.Object);
        var result = await controller.AutenticarPorCPF(cpf);

        Assert.IsInstanceOf<ObjectResult>(result);
        var statusCodeResult = (ObjectResult)result;
        Assert.AreEqual(500, statusCodeResult.StatusCode);
    }
}