using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BasicEcommerce.Migrations
{
    /// <inheritdoc />
    public partial class Inicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    MainImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Description", "MainImageUrl", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { new Guid("42e0396d-66d3-4e3b-a904-5144788b92f3"), "Sonido de alta fidelidad con cancelación de ruido activa y 24 horas de batería.", "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Auriculares Inalámbricos Pro", 129.99m, 75 },
                    { new Guid("573c74a3-2919-459d-8ac1-caed7862bd98"), "Diseño ergonómico, sensor de 16000 DPI y 6 botones programables. Solo 60g.", "https://images.unsplash.com/photo-1605773527852-c546a8584ea3?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Ratón Gaming Ultraligero", 49.99m, 90 },
                    { new Guid("72dfd31b-a004-4714-baa9-8ad42c43e43c"), "Ideal para videollamadas y streaming, con enfoque automático y micrófono integrado.", "https://images.unsplash.com/photo-1636569826709-8e07f6104992?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Webcam Full HD 1080p", 35.75m, 200 },
                    { new Guid("867308cc-968f-40da-89d9-d7dcdb73071d"), "Panel VA con resolución QHD (2560x1440), 144Hz de refresco y 1ms de respuesta.", "https://images.unsplash.com/photo-1666771410003-8437c4781d49?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Monitor Curvo 27'' QHD", 299.00m, 40 },
                    { new Guid("8caf20fb-9e5d-4e74-9e05-df47af133664"), "Diseño ergonómico, compartimentos ocultos y puerto de carga USB integrado.", "https://images.unsplash.com/photo-1668114844900-537ab91478b9?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Mochila Antirrobo para Portátil", 45.80m, 110 },
                    { new Guid("9502ca89-fca4-4898-b94c-c3c9d67f47a3"), "Sonido potente en 360º, 12 horas de batería y resistencia IPX7 al agua.", "https://images.unsplash.com/photo-1589256469067-ea99122bbdc4?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Altavoz Bluetooth Portátil", 59.99m, 150 },
                    { new Guid("9b332067-56d1-4319-a488-dc33d073e1aa"), "Almacenamiento portátil de alta velocidad con conexión USB 3.0. Delgado y resistente.", "https://images.unsplash.com/photo-1613070541337-b40942ee6527?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Disco Duro Externo 2TB", 75.25m, 60 },
                    { new Guid("b116d3b9-007f-4da0-a39e-4eb16aadfa34"), "Carga tu portátil, tablet y smartphone a máxima velocidad. Diseño compacto.", "https://images.unsplash.com/photo-1731616103600-3fe7ccdc5a59?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Cargador Rápido USB-C 65W", 25.00m, 300 },
                    { new Guid("dda144ee-2eec-4357-9359-678285549ac1"), "Diseño compacto con switches Cherry MX Red y retroiluminación RGB personalizable.", "https://images.unsplash.com/photo-1633315754878-b5a3b0ce64f7?q=80&w=2076&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Teclado Mecánico RGB", 89.50m, 120 },
                    { new Guid("f0e0d750-1ec7-40a3-a604-b321c6b99a01"), "Monitor de ritmo cardíaco, GPS integrado y resistencia al agua. Compatible con iOS/Android.", "https://images.unsplash.com/photo-1733570890170-49be2550189b?q=80&w=2071&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Smartwatch Deportivo X1", 99.00m, 85 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
