using System;

namespace BankAccountNS
{
    /// <summary>
    /// Класс, представляющий банковский счет для демонстрационных целей.
    /// </summary>
    public class BankAccount
    {
        private readonly string m_customerName;
        private double m_balance;

        /// <summary>
        /// Сообщение об ошибке, когда сумма дебета превышает баланс.
        /// </summary>
        public const string DebitAmountExceedsBalanceMessage = "Debit amount exceeds balance";

        /// <summary>
        /// Сообщение об ошибке, когда сумма дебета меньше нуля.
        /// </summary>
        public const string DebitAmountLessThanZeroMessage = "Debit amount is less than zero";

        /// <summary>
        /// Сообщение об ошибке, когда сумма кредита меньше нуля.
        /// </summary>
        public const string CreditAmountLessThanZeroMessage = "Credit amount is less than zero";

        /// <summary>
        /// Приватный конструктор для предотвращения создания счета без параметров.
        /// </summary>
        private BankAccount() { }

        /// <summary>
        /// Инициализирует новый экземпляр класса BankAccount с указанными именем клиента и балансом.
        /// </summary>
        /// <param name="customerName">Имя владельца счета.</param>
        /// <param name="balance">Начальный баланс счета.</param>
        public BankAccount(string customerName, double balance)
        {
            m_customerName = customerName;
            m_balance = balance;
        }

        /// <summary>
        /// Получает имя владельца счета.
        /// </summary>
        /// <value>Имя владельца счета.</value>
        public string CustomerName
        {
            get { return m_customerName; }
        }

        /// <summary>
        /// Получает текущий баланс счета.
        /// </summary>
        /// <value>Текущий баланс счета.</value>
        public double Balance
        {
            get { return m_balance; }
        }

        /// <summary>
        /// Снимает указанную сумму со счета.
        /// </summary>
        /// <param name="amount">Сумма для снятия.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Выбрасывается, когда сумма снятия превышает баланс или меньше нуля.
        /// </exception>
        public void Debit(double amount)
        {
            if (amount > m_balance)
            {
                throw new System.ArgumentOutOfRangeException("amount", amount, DebitAmountExceedsBalanceMessage);
            }

            if (amount < 0)
            {
                throw new System.ArgumentOutOfRangeException("amount", amount, DebitAmountLessThanZeroMessage);
            }

            m_balance -= amount; // Исправлено: было +=, теперь правильно -
        }

        /// <summary>
        /// Вносит указанную сумму на счет.
        /// </summary>
        /// <param name="amount">Сумма для внесения.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Выбрасывается, когда сумма внесения меньше нуля.
        /// </exception>
        public void Credit(double amount)
        {
            if (amount < 0)
            {
                throw new System.ArgumentOutOfRangeException("amount", amount, CreditAmountLessThanZeroMessage);
            }

            m_balance += amount;
        }

        /// <summary>
        /// Точка входа в приложение для демонстрации работы класса BankAccount.
        /// </summary>
        public static void Main()
        {
            BankAccount ba = new BankAccount("Mr. Roman Abramovich", 11.99);

            ba.Credit(5.77);
            ba.Debit(11.22);
            Console.WriteLine("Current balance is ${0}", ba.Balance);
            Console.ReadLine();
        }
    }
}