using System;
using System.Threading;


//Con esta clase representamos una cuenta de banco
class CuentaBanco
{
    private decimal balance1;
    private decimal balance2;
    //Inicializamos una nueva instancia de la clase
    public CuentaBanco(decimal initialBalance)
    {
        balance1 = initialBalance;
        balance2 = initialBalance;
    }
    //Creamos el método depositar, el cual hará que se deposite el monto a la cuenta
    public void Depositar(decimal monto)
    {
        lock (this)
        {
            Console.WriteLine("Cliente 1 depositando: " + monto);
            balance1 += monto;
            Console.WriteLine("Nuevo saldo Cliente 1: " + balance1);
        }
    }
    //Creamos el método retiro, el cual retira una cantidad de la cuenta y en caso de que esa cantidad sea mayor al saldo, nos dirá que tenemos saldo insuficiente
    public void Retiro(decimal monto)
    {
        lock (this)
        {
            if (balance2 >= monto)
            {
                Console.WriteLine("Cliente 2 retirando: " + monto);
                balance2 -= monto;
                Console.WriteLine("Nuevo saldo Cliente 2: " + balance2);
            }
            else
            {
                Console.WriteLine("Saldo insuficiente");
            }
        }
    }
}
//Creamos la clase que representa al banco
class SimuladorBanco
{
    static void Main()
    {
        //Creamos una cuenta de banco con un balance de 0$ que será de quien deposita, y otra cuenta con 1000$ que será de quien retira
        CuentaBanco cuenta1 = new CuentaBanco(0);
        CuentaBanco cuenta2 = new CuentaBanco(1000);

        //Creamos dos hilos para depósito y retiro
        Thread depositarThread = new Thread(() =>
        {
            for (int i = 0; i < 6; i++)
            {
                cuenta1.Depositar(100);
                Thread.Sleep(1000);
            }
        });

        Thread retirarThread = new Thread(() =>
        {
            for (int i = 0; i < 6; i++)
            {
                cuenta2.Retiro(200);
                Thread.Sleep(1000);
            }
        });
        //Inicia los hilos
        depositarThread.Start();
        retirarThread.Start();

        //Esperamos a que los hilos se completen
        depositarThread.Join();
        retirarThread.Join();


    }
}

