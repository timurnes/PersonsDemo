using System;
using System.Threading.Tasks;

namespace Persistence {
    public class UnitOfWork : IDisposable {
        private readonly PersonsDemoContext _context;

        private Boolean _disposed = false;

        protected UnitOfWork() { }

        public UnitOfWork(PersonsDemoContext context) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            _context = context;
        }

        internal PersonsDemoContext Context {
            get {
                if (_disposed) {
                    throw new ObjectDisposedException(GetType().FullName);
                }
                return _context;
            }
        }

        public async Task Commit() {
            if (_disposed) {
                throw new ObjectDisposedException(GetType().FullName);
            }

            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(Boolean disposing) {
            if (_disposed) {
                return;
            }

            if (disposing) {
                _context.Dispose();
            }
            _disposed = true;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork() {
            Dispose(false);
        }
    }
}
