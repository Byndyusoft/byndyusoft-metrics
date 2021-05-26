## Для чего?
Пакет использует базовые класса рантайма для организации сбора метрик.
Приложение создает активности в ходе своей работы и любой слушатель может на них подписаться.
Такая организация позволяет отвязать слушателей от бизнес-логики приложения. Запускать приложение как со слушателями, так и без.


## Пример использования
Для генерации событий необходимо использовать ActivitySource. Поле должно быть объявлено приватным и статическим.

    public class MyService
    {
        /// <summary>
        ///     Приватное статическое поле для источника активностей.
        ///     Имя источника должно быть уникальным в рамках приложения, обычно используют полное имя класса или сборки
        /// </summary>
        private static readonly ActivitySource ActivitySource = new ActivitySource(typeof(MyService).FullName);

        public void Foo()
        {
            /// Начало активности, имя активности должно быть уникальным в рамках одного источника.
            /// Активность стартует только если есть подписчики, которые заинтересованы в ней.
            using var activity = ActivitySource.StartActivity(nameof(Foo));

            try
            {
                Console.WriteLine("Foo work");
            }
            catch (Exception exception)
            {
                activity?.SetError();
            }

            //Остановка активности, если она не null при Dispose().
        }
    }
	
Для добавления тегов при создании активности можно использовать перегруженные метод создания активности.
	
	using var activity = ActivitySource.StartActivity(nameof(Foo), new ActivityTags().AddTag("Test", "Value"));

Для прослушивания событий создаем прослушиватель, он должен быть унаследован от ActivityListenerBase

    public class MyListener : ActivityListenerBase
    {
        /// <summary>
        ///     Обрабатываем начало активности. Логируем, собираем метрики, что угодно.
        /// </summary>
        protected override void ActivityStarted(Activity activity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Обрабатываем остановку активности. Логируем, собираем метрики, что угодно.
        /// </summary>
        protected override void ActivityStopped(Activity activity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Отбираем источники, из которых мы будет слушать события
        /// </summary>
        protected override bool ShouldListenTo(ActivitySource activitySource)
        {
            throw new NotImplementedException();
        }
    }
	
Для получения значения тегов, переданных при создании активности необходимо использовать метод расширения активности.

    activity.GetTagObject("Test")

Почти все готово, для старта сбора метрик необходимо зарегистрировать созданный слушатель в Startup в ConfigureServices.

    public void ConfigureServices(IServiceCollection services)
        {
            //...

            services.AddApplicationMetrics()
                    .AddMetricListener<MyListener>();

            //...
        }
