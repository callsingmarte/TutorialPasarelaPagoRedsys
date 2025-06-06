using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BasicEcommerce.Migrations
{
    /// <inheritdoc />
    public partial class Orders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("42e0396d-66d3-4e3b-a904-5144788b92f3"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("573c74a3-2919-459d-8ac1-caed7862bd98"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("72dfd31b-a004-4714-baa9-8ad42c43e43c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("867308cc-968f-40da-89d9-d7dcdb73071d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("8caf20fb-9e5d-4e74-9e05-df47af133664"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("9502ca89-fca4-4898-b94c-c3c9d67f47a3"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("9b332067-56d1-4319-a488-dc33d073e1aa"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("b116d3b9-007f-4da0-a39e-4eb16aadfa34"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("dda144ee-2eec-4357-9359-678285549ac1"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("f0e0d750-1ec7-40a3-a604-b321c6b99a01"));

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientMail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Description", "MainImageUrl", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { new Guid("0f05c49c-4bf6-4623-b3f7-7c1bd0394fd0"), "Sonido de alta fidelidad con cancelación de ruido activa y 24 horas de batería.", "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Auriculares Inalámbricos Pro", 129.99m, 75 },
                    { new Guid("33728ad3-f11d-4afd-ba7d-38d2f0516e2a"), "Panel VA con resolución QHD (2560x1440), 144Hz de refresco y 1ms de respuesta.", "https://images.unsplash.com/photo-1666771410003-8437c4781d49?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Monitor Curvo 27'' QHD", 299.00m, 40 },
                    { new Guid("41dd73d7-6eaa-4438-bcdc-277583e64ab9"), "Ideal para videollamadas y streaming, con enfoque automático y micrófono integrado.", "https://images.unsplash.com/photo-1636569826709-8e07f6104992?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Webcam Full HD 1080p", 35.75m, 200 },
                    { new Guid("51adcc72-d012-4f15-889b-d5b5cf3328ed"), "Diseño ergonómico, sensor de 16000 DPI y 6 botones programables. Solo 60g.", "https://images.unsplash.com/photo-1605773527852-c546a8584ea3?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Ratón Gaming Ultraligero", 49.99m, 90 },
                    { new Guid("7e9b81b5-3f28-4009-bbfc-517c7149ae4d"), "Carga tu portátil, tablet y smartphone a máxima velocidad. Diseño compacto.", "https://images.unsplash.com/photo-1731616103600-3fe7ccdc5a59?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Cargador Rápido USB-C 65W", 25.00m, 300 },
                    { new Guid("928f4f5b-72cd-4782-9a23-1fb243e0a67b"), "Diseño compacto con switches Cherry MX Red y retroiluminación RGB personalizable.", "https://images.unsplash.com/photo-1633315754878-b5a3b0ce64f7?q=80&w=2076&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Teclado Mecánico RGB", 89.50m, 120 },
                    { new Guid("c76a62cf-e195-46a8-a0b9-44712d4a77bb"), "Monitor de ritmo cardíaco, GPS integrado y resistencia al agua. Compatible con iOS/Android.", "https://images.unsplash.com/photo-1733570890170-49be2550189b?q=80&w=2071&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Smartwatch Deportivo X1", 99.00m, 85 },
                    { new Guid("ccdda0b5-c978-4ed8-90b0-d4be59a48807"), "Diseño ergonómico, compartimentos ocultos y puerto de carga USB integrado.", "https://images.unsplash.com/photo-1668114844900-537ab91478b9?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Mochila Antirrobo para Portátil", 45.80m, 110 },
                    { new Guid("cfc43279-f2ea-4c07-a174-ee8312e8f26f"), "Almacenamiento portátil de alta velocidad con conexión USB 3.0. Delgado y resistente.", "https://images.unsplash.com/photo-1613070541337-b40942ee6527?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Disco Duro Externo 2TB", 75.25m, 60 },
                    { new Guid("e2c2aae8-ad76-4c04-98a3-54dedae2d5cc"), "Sonido potente en 360º, 12 horas de batería y resistencia IPX7 al agua.", "https://images.unsplash.com/photo-1589256469067-ea99122bbdc4?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Altavoz Bluetooth Portátil", 59.99m, 150 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("0f05c49c-4bf6-4623-b3f7-7c1bd0394fd0"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("33728ad3-f11d-4afd-ba7d-38d2f0516e2a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("41dd73d7-6eaa-4438-bcdc-277583e64ab9"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("51adcc72-d012-4f15-889b-d5b5cf3328ed"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("7e9b81b5-3f28-4009-bbfc-517c7149ae4d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("928f4f5b-72cd-4782-9a23-1fb243e0a67b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("c76a62cf-e195-46a8-a0b9-44712d4a77bb"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("ccdda0b5-c978-4ed8-90b0-d4be59a48807"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("cfc43279-f2ea-4c07-a174-ee8312e8f26f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("e2c2aae8-ad76-4c04-98a3-54dedae2d5cc"));

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
    }
}
