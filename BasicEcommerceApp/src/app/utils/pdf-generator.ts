// src/app/utils/pdf-generator.ts
import { jsPDF } from 'jspdf';
import { autoTable } from 'jspdf-autotable'; // ¡Importa esto para extender jsPDF con autoTable!
import { IOrder } from '../models/order.model'; // Asegúrate de que esta ruta es correcta
import { IOrderItem } from '../models/orderItem.model';

/**
 * Genera un PDF simple para un solo pedido, usando jspdf-autotable para los ítems.
 * @param order Los datos del pedido a incluir en el PDF.
 * @param filename El nombre del archivo PDF (sin extensión).
 */
export function generateOrderPdf(order: IOrder, filename: string = 'pedido'): void {
  const doc = new jsPDF(); // Inicializa el documento PDF

  let y = 10; // Posición inicial en el eje Y
  const margin = 15; // Margen izquierdo/derecho (aumentado un poco para mejor lectura)
  const pageWidth = doc.internal.pageSize.getWidth();

  // --- Encabezado del Documento ---
  doc.setFontSize(24);
  doc.text('Resumen del Pedido', pageWidth / 2, y, { align: 'center' });
  y += 10;
  doc.setFontSize(10);
  doc.text('Fecha de generación: ' + new Date().toLocaleDateString('es-ES', { day: '2-digit', month: '2-digit', year: 'numeric', hour: '2-digit', minute: '2-digit' }), margin, y);
  y += 15;

  // --- Información General del Pedido ---
  doc.setFontSize(16);
  doc.text('Detalles del Pedido:', margin, y);
  y += 8; // Espacio
  doc.setFontSize(12);
  doc.text(`ID de Pedido: ${order.orderId}`, margin, y);
  y += 7;
  doc.text(`ID Redsys: ${order.redsysOrderId || 'N/A'}`, margin, y);
  y += 7;
  doc.text(`Correo Cliente: ${order.clientMail}`, margin, y);
  y += 7;
  doc.text(`Fecha Pedido: ${new Date(order.orderDate).toLocaleDateString('es-ES')} ${new Date(order.orderDate).toLocaleTimeString('es-ES')}`, margin, y);
  y += 7;
  doc.text(`Estado: ${order.status}`, margin, y);
  y += 15; // Espacio antes de la siguiente sección

  // --- Detalles de los Artículos del Pedido (usando autoTable) ---
  doc.setFontSize(16);
  doc.text('Artículos del Pedido:', margin, y);
  y += 8;

  // Definir las cabeceras de la tabla
  const tableHeaders = [['Producto', 'Descripción', 'Cantidad', 'Precio Unit.', 'Subtotal']];

  // Preparar los datos de las filas de la tabla
  const tableData = order.orderItems?.map((item: IOrderItem) => [
    item.product?.name || 'Producto Desconocido',
    item.product?.description || 'N/A', // Muestra "N/A" si no hay descripción
    item.quantity.toString(),
    `${item.product?.price?.toFixed(2) || '0.00'} €`,
    `${item.subtotal?.toFixed(2) || '0.00'} €`
  ]) || [];

  // Configurar y generar la tabla
  autoTable(doc, {
    head: tableHeaders,
    body: tableData,
    startY: y, // Inicia la tabla en la posición actual 'y'
    theme: 'striped', // Tema de la tabla: 'striped', 'grid', 'plain'
    styles: {
      fontSize: 10,
      cellPadding: 2,
      overflow: 'linebreak' // Permite que el texto largo se rompa en varias líneas
    },
    headStyles: {
      fillColor: [30, 144, 255], // Color de fondo del encabezado (azul celeste)
      textColor: [255, 255, 255], // Color del texto del encabezado (blanco)
      fontStyle: 'bold',
      halign: 'center' // Centra el texto en las cabeceras
    },
    columnStyles: {
      0: { cellWidth: 50 }, // Producto (ancho fijo)
      1: { cellWidth: 'auto' }, // Descripción (ancho automático)
      2: { cellWidth: 20, halign: 'center' }, // Cantidad (ancho fijo, centrado)
      3: { cellWidth: 25, halign: 'right' }, // Precio Unitario (ancho fijo, alineado a la derecha)
      4: { cellWidth: 25, halign: 'right' }  // Subtotal (ancho fijo, alineado a la derecha)
    },
    margin: { left: margin, right: margin }, // Margen para la tabla
    didDrawPage: (data: any) => {
      // Opcional: añadir un pie de página con el número de página en cada página
      doc.setFontSize(10);
      const pageCount = doc.getNumberOfPages();
      doc.text(`Página ${data.pageNumber} de ${pageCount}`, data.settings.margin.left, doc.internal.pageSize.height - 10);
    }
  });

  // Actualizar la posición 'y' al final de la tabla + un espacio
  y = (doc as any).lastAutoTable.finalY + 15;

  // --- Total del Pedido ---
  // Añadir una nueva página si el total queda muy cerca del final de la página actual
  if (y > doc.internal.pageSize.height - 30) {
    doc.addPage();
    y = margin;
  }
  doc.setFontSize(18);
  doc.setTextColor(30, 144, 255); // Azul celeste
  doc.text(`TOTAL DEL PEDIDO: ${order.totalPrice.toFixed(2)} €`, pageWidth - margin, y, { align: 'right' });
  doc.setTextColor(0, 0, 0); // Volver al color de texto predeterminado

  // --- Guardar el PDF ---
  doc.save(`${filename}.pdf`);
}
