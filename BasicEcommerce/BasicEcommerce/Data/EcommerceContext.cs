using BasicEcommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicEcommerce.Data
{
    public class EcommerceContext : DbContext
    {
        public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<Product> products = new List<Product>()
            {
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Auriculares Inalámbricos Pro",
                    Description = "Sonido de alta fidelidad con cancelación de ruido activa y 24 horas de batería.",
                    Price = 129.99m, // La 'm' es crucial para los literales decimales
                    Stock = 75,
                    MainImageUrl = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Teclado Mecánico RGB",
                    Description = "Diseño compacto con switches Cherry MX Red y retroiluminación RGB personalizable.",
                    Price = 89.50m,
                    Stock = 120,
                    MainImageUrl = "https://images.unsplash.com/photo-1633315754878-b5a3b0ce64f7?q=80&w=2076&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Ratón Gaming Ultraligero",
                    Description = "Diseño ergonómico, sensor de 16000 DPI y 6 botones programables. Solo 60g.",
                    Price = 49.99m,
                    Stock = 90,
                    MainImageUrl = "https://images.unsplash.com/photo-1605773527852-c546a8584ea3?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Monitor Curvo 27'' QHD",
                    Description = "Panel VA con resolución QHD (2560x1440), 144Hz de refresco y 1ms de respuesta.",
                    Price = 299.00m,
                    Stock = 40,
                    MainImageUrl = "https://images.unsplash.com/photo-1666771410003-8437c4781d49?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Webcam Full HD 1080p",
                    Description = "Ideal para videollamadas y streaming, con enfoque automático y micrófono integrado.",
                    Price = 35.75m,
                    Stock = 200,
                    MainImageUrl = "https://images.unsplash.com/photo-1636569826709-8e07f6104992?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Disco Duro Externo 2TB",
                    Description = "Almacenamiento portátil de alta velocidad con conexión USB 3.0. Delgado y resistente.",
                    Price = 75.25m,
                    Stock = 60,
                    MainImageUrl = "https://images.unsplash.com/photo-1613070541337-b40942ee6527?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Smartwatch Deportivo X1",
                    Description = "Monitor de ritmo cardíaco, GPS integrado y resistencia al agua. Compatible con iOS/Android.",
                    Price = 99.00m,
                    Stock = 85,
                    MainImageUrl = "https://images.unsplash.com/photo-1733570890170-49be2550189b?q=80&w=2071&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Altavoz Bluetooth Portátil",
                    Description = "Sonido potente en 360º, 12 horas de batería y resistencia IPX7 al agua.",
                    Price = 59.99m,
                    Stock = 150,
                    MainImageUrl = "https://images.unsplash.com/photo-1589256469067-ea99122bbdc4?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Cargador Rápido USB-C 65W",
                    Description = "Carga tu portátil, tablet y smartphone a máxima velocidad. Diseño compacto.",
                    Price = 25.00m,
                    Stock = 300,
                    MainImageUrl = "https://images.unsplash.com/photo-1731616103600-3fe7ccdc5a59?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Mochila Antirrobo para Portátil",
                    Description = "Diseño ergonómico, compartimentos ocultos y puerto de carga USB integrado.",
                    Price = 45.80m,
                    Stock = 110,
                    MainImageUrl = "https://images.unsplash.com/photo-1668114844900-537ab91478b9?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                }
            };

            modelBuilder.Entity<Product>().HasData(products);
        }
    }
}
