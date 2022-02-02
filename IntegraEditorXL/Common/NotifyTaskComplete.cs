using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace IntegraEditorXL.Common
{
    public sealed class NotifyTaskComplete<T> : INotifyPropertyChanged
    {
        public NotifyTaskComplete(Task<T> task)
        {
            Task = task;

            if(!task.IsCompleted)
            {
                var _ = WatchTask(task);
            }
        }

        private async Task WatchTask(Task task)
        {
            try
            {
                await task;
            }
            catch { }

            var propertyChanged = PropertyChanged;
            if (propertyChanged == null)
                return;

            propertyChanged(this, new PropertyChangedEventArgs(nameof(Status)));
            propertyChanged(this, new PropertyChangedEventArgs(nameof(IsCompleted)));
            propertyChanged(this, new PropertyChangedEventArgs(nameof(IsLoading)));

            if (task.IsCanceled)
            {
                propertyChanged(this, new PropertyChangedEventArgs(nameof(IsCanceled)));
            }
            else if (task.IsFaulted)
            {
                propertyChanged(this, new PropertyChangedEventArgs(nameof(IsFaulted)));
                propertyChanged(this, new PropertyChangedEventArgs(nameof(Exception)));
                propertyChanged(this, new PropertyChangedEventArgs(nameof(InnerException)));
                propertyChanged(this, new PropertyChangedEventArgs(nameof(ErrorMessage)));
            }
            else
            {
                propertyChanged(this, new PropertyChangedEventArgs(nameof(IsSuccessfullyCompleted)));
                propertyChanged(this, new PropertyChangedEventArgs(nameof(Result)));
            }

        }

        public Task<T> Task { get; private set; }

        public T Result { get => (Task.Status == TaskStatus.RanToCompletion) ? Task.Result : default(T); }

        public TaskStatus Status { get { return Task.Status; } }

        public bool IsCompleted { get { return Task.IsCompleted; } }
        public bool IsLoading { get { return !Task.IsCompleted; } }
        public bool IsSuccessfullyCompleted { get => Task.Status == TaskStatus.RanToCompletion; }
        public bool IsCanceled { get { return Task.IsCanceled; } }
        public bool IsFaulted { get { return Task.IsFaulted; } }

        public AggregateException Exception { get { return Task.Exception; } }
        public Exception InnerException { get => (Exception == null) ? null : Exception.InnerException; }

        public string ErrorMessage { get => (InnerException == null ? null : InnerException.Message); }



        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }


}
