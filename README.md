# Entendiendo Logging en Aplicaciones .NET con Log4net

Esta entrada se relaciona con esta entrada anterior, que trata también sobre el tema de Logging.
Log4net es opción altamente recomendable para la implementación del logging en las aplicaciones desarrolladas para .NET. Sobre todo cuando se necesita una solución más configurable y robusta que la proporcionada por las clases del ensamblado System.Diagnostics, que en comparación con log4net están en un nivel elemental, si necesitamos una herramienta que soporte diferentes fuentes de persistencia, que no afecte el desempeño de las aplicaciones y que sea transportable entre la implementación .NET de Microsoft y la del proyecto Mono.

Log4net es un Framework open source creado por la fundación Apache basado en la implementación de los servicios de logging existentes en log4j, un componente de logging usado durante años en los ecosistemas Java.
La arquitectura de Log4net puede resumirse en tres clases principales cada una encargada de una responsabilidad dentro del Framework y que se detalla a continuación:
Logger Captura la información para la bitácora.
Appender Publica la información hacia diversas fuentes de logging configuradas en la aplicación. Al menos debe definirse un Appender, el Appender predeterminado es el ConsoleAppender cual dirige su salida hacia una terminal de Consola.
Layout Se utiliza para darle formato haciendo legible cada salida de los distintos Appenders.

Para más información consultar http://logging.apache.org/log4j/1.2/manual.html aunque es para log4j la arquitectura es idéntica a log4net.
Como ejemplo del uso de log4net vamos a crear dos programas un cliente y un servidor los cuales se comunicarán entre sí, la clase ServerProgram es un servidor TCP que escucha en el puerto 6060 y la clase Program que es el cliente que al conectarse con el servidor, le solicita al usuario el nombre de un archivo de texto el cuál sera leído y cada línea de texto será enviada hacia el servidor. El programa cliente implementa todo el código básico para el manejo del logging con log4net.


Código de la clase ServerProgram


Código de la clase Program (cliente)



El archivo de configuración App.config en donde van las opciones de configuración de Log4net



En el archivo de configuración utilizamos el declarado en la siguiente sección
<appender name="FileAppender" type="log4net.Appender.FileAppender">
<file value="log.txt"/>
<appendToFile value="true"/>
<layout type="log4net.Layout.PatternLayout">
<conversionPattern value="[%date{dd-MM-yyyy HH:mm:ss}] [%level] %message %newline"/>
</layout>
</appender>
Declaración del componente principal para implementar los métodos del Logger.
static readonly log4net.ILog log = log4net.LogManager.GetLogger(
System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
carga la configuración del archivo de configuración para Log4net.
XmlConfigurator.Configure();
Escribir una excepción al log.
log.Error(ex.Message, ex);
Escribir información al log
log.Info("Ejecutando la aplicación en " + DateTime.Now.ToLongTimeString());
Compilando los programas:



Ejecutando el servidor



Ejecutando el cliente



Generando excepción de formato



Generando excepción de comunicación



Revisando el archivo de log, creado por log4net

