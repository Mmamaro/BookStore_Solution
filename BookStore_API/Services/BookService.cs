using BookStore_API.Repositories;
using System.ComponentModel;
using System.Threading;

namespace BookStore_API.Services
{
    public class BookService : IHostedService
    {
        private readonly BookRepository? _bookRepository;

        public BookService(BookRepository bookRepo)
        {
           _bookRepository = bookRepo;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() => CalculateAndUpdateTotalPrice(cancellationToken));
            Task.Run(() => CalculateAndUpdateDaysPublished(cancellationToken));

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(Timeout.Infinite, cancellationToken);
        }

        private async Task CalculateAndUpdateTotalPrice(CancellationToken cancellationToken)
        {
            while(!cancellationToken.IsCancellationRequested)
            {
                var books = await _bookRepository.GetAllBooksAsync();

                foreach (var book in books)
                {
                    book.TotalPrice = book.Price * 1.15m;

                    await _bookRepository.UpdateBookTotalPrice(book.BookId, book.TotalPrice);

                    Console.WriteLine("{0} costs: {1}", book.BookName, book.TotalPrice);
                }

                await Task.Delay(TimeSpan.FromHours(12), cancellationToken);
            }

            
        }

        private async Task CalculateAndUpdateDaysPublished(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var books = await _bookRepository.GetAllBooksAsync();

                foreach (var book in books)
                {
                    book.DaysPublished = (int)(DateTime.Now - book.PublishDate).TotalDays;

                    await _bookRepository.UpdateBookDaysPublished(book.BookId, book.DaysPublished);

                    Console.WriteLine("{0} was published: {1} ago", book.BookName, book.DaysPublished);
                }

                await Task.Delay(TimeSpan.FromHours(24), cancellationToken);
            }


        }
    }
}
