# Minuta de relevamiento
## Contexto
Una empresa de tecnología decide adquirir una página web por la cual pueda vender sus productos al público. El dueño de la empresa podrá cargar los productos para que los clientes puedan visualizar y seleccionar lo que deseen adquirir.

## Proceso con el sistema deseado:
Decidimos crear un programa en el cual los usuarios (clientes y admin) puedan registrarse con un nombre de usuario y una contraseña, solicitando ingresar un email y su dirección.
Al iniciar la sesión en la página, el usuario “admin” podrá agregar productos, modificar el stock de los mismos y eliminarlos si es necesario.
El usuario “cliente” podrá realizar una compra seleccionando el/ los producto/s que desea y realizar la compra optando por el método de pago de su preferencia.
Todos los usuarios, tendrán a disposición visualizar la lista de productos con su precio correspondiente, y al mismo tiempo, poder aplicar filtros sobre la misma.

## Lista de requerimientos y casos de uso:
Usuario **Admin**:
- Registrarse con el nombre de usuario y contraseña deseada, ingresando un email y la dirección.
- Iniciar sesión con los datos correspondientes (Login).
- Agregar un producto, ingresando su id, información, precio y stock disponible.
- Modificar el producto (Descripción, precio y stock).
- Eliminar el producto.
- Filtrar productos según categorías.
- Recibir información de venta realizada.
- Cerrar sesión (Logout).

Usuario **Client**:
- Registrarse con el nombre de usuario y contraseña deseada, ingresando un email y la dirección.
- Iniciar sesión con los datos correspondientes (Login).
- Filtrar productos según categorías.
- Seleccionar el producto deseado.
- Realizar la compra eligiendo el método de pago.
- Cerrar sesión (Logout).
