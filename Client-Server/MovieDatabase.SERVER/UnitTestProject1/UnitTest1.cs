using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MovieDatabase.Domain.Managers;
using MovieDatabase.Model;
using MovieDatabase.Server;
using MovieDatabase.DataAccess.Repository;
using System;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddMovieMethodFromRepositorySouldBeCalled()
        {
            var mockRepository = new Mock<IMovieRepository>();

            mockRepository.Setup(x => x.AddMovie(It.IsAny<Movie>()));

            var moviesManager = new MoviesManager(mockRepository.Object);

            moviesManager.AddMovie(new Movie());

            mockRepository.VerifyAll();

        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ConnectionTest()
        {
            var mockConnection = new Mock<IConnection>();

            
            //var asyncResult = new Mock<IAsyncResult>();
          
            mockConnection.Setup(x => x.AcceptCallback(It.IsAny<AsyncResult>(), It.IsAny<Socket>()));


            Program.AcceptCallback(It.IsAny<AsyncResult>());

            mockConnection.VerifyAll();

        }

    }
}
