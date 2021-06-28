/*
 * Refactorear la clase para respetar principios de programación orientada a objetos. Qué pasa si debemos soportar un nuevo idioma para los reportes, o
 * agregar más formas geométricas?
 *
 * Se puede hacer cualquier cambio que se crea necesario tanto en el código como en los tests. La única condición es que los tests pasen OK.
 *
 * TODO: Implementar Trapecio/Rectangulo, agregar otro idioma a reporting.
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodingChallenge.Data.Classes
{
    public class FormaGeometrica
    {
        #region Formas

        public const int Cuadrado = 1;
        public const int TrianguloEquilatero = 2;
        public const int Circulo = 3;
        public const int Trapecio = 4;
        public const int Rectangulo = 5;

        #endregion

        #region Idiomas

        public const int Castellano = 1;
        public const int Ingles = 2;
        public const int Portugues = 3;

        #endregion

        private readonly decimal _lado;
        private readonly decimal _lado3;
        private readonly decimal _altura;
        private readonly decimal _ladomayor;

        public int Tipo { get; set; }

        public FormaGeometrica(int tipo, decimal ancho, decimal altura=0, decimal largo=0, decimal lado3=0)
        {
            Tipo = tipo;
            _lado = ancho;
            _altura = altura;
            _ladomayor = largo;
            _lado3 = lado3;
        }

        public static string Imprimir(List<FormaGeometrica> formas, int idioma)
        {
            var sb = new StringBuilder();

            if (!formas.Any())
            {
                switch (idioma)
                {
                    case Castellano: sb.Append("<h1>Lista vacía de formas!</h1>");
                        break;
                    case Ingles: sb.Append("<h1>Empty list of shapes!</h1>");
                        break;
                    case Portugues: sb.Append("<h1>Lista vazia de formulários!</h1>");
                        break;
                }               
            }
            else
            {
                switch (idioma)
                {
                    case Castellano:
                        sb.Append("<h1>Reporte de Formas</h1>");
                        break;
                    case Ingles:
                        sb.Append("<h1>Shapes report</h1>");
                        break;
                    case Portugues:
                        sb.Append("<h1>Relatório de formulários</h1>");
                        break;
                }                

                var numeroCuadrados = 0;
                var numeroCirculos = 0;
                var numeroTriangulos = 0;
                var numeroTrapecios = 0;
                var numeroRectangulos = 0;

                var areaCuadrados = 0m;
                var areaCirculos = 0m;
                var areaTriangulos = 0m;
                var areaTrapecios = 0m;
                var areaRectangulos = 0m;

                var perimetroCuadrados = 0m;
                var perimetroCirculos = 0m;
                var perimetroTriangulos = 0m;
                var perimetroTrapecios = 0m;
                var perimetroRectangulos = 0m;

                for (var i = 0; i < formas.Count; i++)
                {
                    switch (formas[i].Tipo )
                    {
                        case Cuadrado:
                            numeroCuadrados++;
                            areaCuadrados += formas[i].CalcularArea();
                            perimetroCuadrados += formas[i].CalcularPerimetro();
                            break;
                        case Circulo:
                            numeroCirculos++;
                            areaCirculos += formas[i].CalcularArea();
                            perimetroCirculos += formas[i].CalcularPerimetro();
                            break;
                        case TrianguloEquilatero:
                            numeroTriangulos++;
                            areaTriangulos += formas[i].CalcularArea();
                            perimetroTriangulos += formas[i].CalcularPerimetro();
                            break;
                        case Trapecio:
                            numeroTrapecios++;
                            areaTrapecios += formas[i].CalcularArea();
                            perimetroTrapecios += formas[i].CalcularPerimetro();
                            break;
                        case Rectangulo:
                            numeroRectangulos++;
                            areaRectangulos+= formas[i].CalcularArea();
                            perimetroRectangulos += formas[i].CalcularPerimetro();
                            break;
                    }
                    
                }
                
                sb.Append(ObtenerLinea(numeroCuadrados, areaCuadrados, perimetroCuadrados, Cuadrado, idioma));
                sb.Append(ObtenerLinea(numeroCirculos, areaCirculos, perimetroCirculos, Circulo, idioma));
                sb.Append(ObtenerLinea(numeroTriangulos, areaTriangulos, perimetroTriangulos, TrianguloEquilatero, idioma));                
                sb.Append(ObtenerLinea(numeroTrapecios, areaTrapecios, perimetroTrapecios, Trapecio, idioma));
                sb.Append(ObtenerLinea(numeroRectangulos, areaRectangulos, perimetroRectangulos, Rectangulo, idioma));

                // FOOTER
                sb.Append("TOTAL:<br/>");
                sb.Append(numeroCuadrados + numeroCirculos + numeroTriangulos +numeroTrapecios+numeroRectangulos+ " " + 
                    (idioma == Castellano ? "formas" : (idioma == Ingles ? "shapes":"formas")) + " ");
                sb.Append((idioma == Castellano ? "Perimetro " : (idioma == Ingles ? "Perimeter ": "Perímetro ")) 
                            + (perimetroCuadrados + perimetroTriangulos + perimetroCirculos+perimetroRectangulos+perimetroTrapecios).ToString("#.##") + " ");
                sb.Append("Area " + (areaCuadrados + areaCirculos + areaTriangulos+areaRectangulos+areaTrapecios).ToString("#.##"));
            }

            return sb.ToString();
        }

        private static string ObtenerLinea(int cantidad, decimal area, decimal perimetro, int tipo, int idioma)
        {
            if (cantidad > 0)
            {
                switch (idioma)
                {
                    case Castellano:
                        return $"{cantidad} {TraducirForma(tipo, cantidad, idioma)} | Area {area:#.##} | Perimetro {perimetro:#.##} <br/>";
                    case Ingles:
                        return $"{cantidad} {TraducirForma(tipo, cantidad, idioma)} | Area {area:#.##} | Perimeter {perimetro:#.##} <br/>";
                    case Portugues:
                        return $"{cantidad} {TraducirForma(tipo, cantidad, idioma)} | Área {area:#.##} | Perímetro {perimetro:#.##} <br/>";
                }               
            }

            return string.Empty;
        }

        private static string TraducirForma(int tipo, int cantidad, int idioma)
        {
            switch (tipo)
            {
                case Cuadrado:
                    if (idioma == Castellano) return cantidad == 1 ? "Cuadrado" : "Cuadrados";
                    else if(idioma==Ingles) return cantidad == 1 ? "Square" : "Squares";
                    else return cantidad == 1 ? "Quadrado" : "Quadrados"; //Portugues
                case Circulo:
                    if (idioma == Castellano) return cantidad == 1 ? "Círculo" : "Círculos";
                    else if(idioma==Ingles) return cantidad == 1 ? "Circle" : "Circles";
                    else return cantidad == 1 ? "Círculo" : "Círculos"; //Portugues
                case TrianguloEquilatero:
                    if (idioma == Castellano) return cantidad == 1 ? "Triángulo" : "Triángulos";
                    else if(idioma==Ingles) return cantidad == 1 ? "Triangle" : "Triangles";
                    else return cantidad == 1 ? "Triângulo" : "Triângulos"; //Portugues
                case Rectangulo:
                    if (idioma == Castellano) return cantidad == 1 ? "Rectangulo" : "Rectangulos";
                    else if (idioma == Ingles) return cantidad == 1 ? "Rectangle" : "Rectangles";
                    else return cantidad == 1 ? "Retângulo" : "Retângulos"; //Portugues
                case Trapecio:
                    if (idioma == Castellano) return cantidad == 1 ? "Trapecio" : "Trapecios";
                    else if (idioma == Ingles) return cantidad == 1 ? "Trapeze" : "Trapezoids";
                    else return cantidad == 1 ? "Trapézio" : "Trapézio"; //Portugues
            }

            return string.Empty;
        }

        public decimal CalcularArea()
        {
            switch (Tipo)
            {
                case Cuadrado: return _lado * _lado; // AreaPoligonoRegular(4);
                case Circulo: return (decimal)Math.PI * (_lado / 2) * (_lado / 2);
                case TrianguloEquilatero: return ((decimal)Math.Sqrt(3) / 4) * _lado * _lado; //AreaPoligonoRegular(3)
                case Rectangulo: return _ladomayor * _lado;
                case Trapecio: return (_ladomayor + _lado) * _altura / 2;
                default:
                    throw new ArgumentOutOfRangeException(@"Forma desconocida");
            }
        }
        public decimal AreaPoligonoRegular(int n)
        {
            //Area=(Perimetro * Apotema) / 2
            //Area= n*lado*lado / 4*tan(180/n)
            //n cantidad de lados del poligono, _lado medida de un lado
            return (n * _lado * _lado) / (4 * (decimal)Math.Tan(180 / n));            
        }
        public decimal CalcularPerimetro()
        {
            switch (Tipo)
            {
                case Cuadrado: return _lado * 4;
                case Circulo: return (decimal)Math.PI * _lado;
                case TrianguloEquilatero: return _lado * 3;
                case Rectangulo: return (_lado * 2) + (_ladomayor * 2);
                case Trapecio: return _lado + _ladomayor + (2 * _lado3); //trapecio isosceles, en este caso _lado3 seria el tercer lado
                default:
                    throw new ArgumentOutOfRangeException(@"Forma desconocida");
            }
        }
    }
}
