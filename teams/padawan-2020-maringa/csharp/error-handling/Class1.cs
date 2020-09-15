using System;
using Xunit;

public class ErrorHandlingTests
{
    // Read more about exception handling here:
    // https://msdn.microsoft.com/en-us/library/ms173162.aspx?f=255&MSPPError=-2147217396
    [Fact]
    public void ThrowException()
    {
        Assert.Throws<Exception>(() => ErrorHandling.HandleErrorByThrowingException());
    }

    // Read more about nullable types here:
    // https://msdn.microsoft.com/en-us/library/1t3y8s4s.aspx?f=255&MSPPError=-2147217396
    [Fact]
    public void ReturnNullableType()
    {
        var successfulResult = ErrorHandling.HandleErrorByReturningNullableType("1");
        Assert.Equal(1, successfulResult);

        var failureResult = ErrorHandling.HandleErrorByReturningNullableType("a");
        Assert.Null(failureResult);
    }

    // Read more about out parameters here:
    // https://msdn.microsoft.com/en-us/library/t3c3bfhx.aspx?f=255&MSPPError=-2147217396
    [Fact]
    public void ReturnWithOutParameter()
    {
        int result;
        var successfulResult = ErrorHandling.TryHandleErrorWithOutParam("1", out result);
        Assert.True(successfulResult);
        Assert.Equal(1, result);

        var failureResult = ErrorHandling.TryHandleErrorWithOutParam("a", out _);
        Assert.False(failureResult);
        // The value of result is meaningless here (it could be anything) so it shouldn't be used and it's not validated 
    }

    private class DisposableResource : IDisposable
    {
        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            IsDisposed = true;
        }
    }

    public class DisposableResourceBase : IDisposable
    {

        public bool IsDisposed { get; private set; }

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                IsDisposed = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~DisposableResourceBase()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    public class DisposableResource2 : DisposableResourceBase
    {
        public bool IsDisposed2 { get; private set; }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            IsDisposed2 = true;
        }
    }

    // Read more about IDisposable here:
    // https://msdn.microsoft.com/en-us/library/system.idisposable(v=vs.110).aspx
    [Fact]
    public void DisposableObjectsAreDisposedWhenThrowingAnException()
    {
        var disposableResource = new DisposableResource();


        Assert.False(disposableResource.IsDisposed);
        Assert.Throws<Exception>(() => ErrorHandling.DisposableResourcesAreDisposedWhenExceptionIsThrown(disposableResource));
        Assert.True(disposableResource.IsDisposed);
    }

    // Read more about IDisposable here:
    // https://msdn.microsoft.com/en-us/library/system.idisposable(v=vs.110).aspx
    [Fact]
    public void DisposableObjectsAreDisposedWhenThrowingAnException2()
    {
        var disposableResource = new DisposableResource();


        Assert.Equal(1, ErrorHandling.DisposableResourcesAreDisposedWhenExceptionIsThrown("1", disposableResource));
        Assert.False(disposableResource.IsDisposed);

        Assert.Throws<FormatException>(() => ErrorHandling.DisposableResourcesAreDisposedWhenExceptionIsThrown("a", disposableResource));
        Assert.True(disposableResource.IsDisposed);
    }

    // Read more about IDisposable here:
    // https://msdn.microsoft.com/en-us/library/system.idisposable(v=vs.110).aspx
    [Fact]
    public void DisposableObjectsAreDisposedWhenThrowingAnException3()
    {
        var disposableResource = new DisposableResource();

        Assert.False(disposableResource.IsDisposed);
        Assert.Equal(1, ErrorHandling.DisposableResourcesAreDisposedWhenExceptionIsThrown2("1", disposableResource));
        Assert.True(disposableResource.IsDisposed);

        disposableResource = new DisposableResource();

        Assert.False(disposableResource.IsDisposed);
        Assert.Throws<FormatException>(() => ErrorHandling.DisposableResourcesAreDisposedWhenExceptionIsThrown2("a", disposableResource));
        Assert.True(disposableResource.IsDisposed);
    }
}