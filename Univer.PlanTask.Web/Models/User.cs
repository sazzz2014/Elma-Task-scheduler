using System;
using System.Collections.Generic;

namespace Univer.PlanTask.Web.Models
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public virtual long Id { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        public virtual string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public virtual string Password { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public virtual string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public virtual string SecondName { get; set; }

        /// <summary>
        /// День рождения
        /// </summary>
        public virtual DateTime? BirthDay { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public virtual UserStatus Status { get; set; }

        /// <summary>
        /// Задачи
        /// </summary>
        public virtual ICollection<Task> Tasks { get; set; }
    }

    /// <summary>
    /// Статус пользователя
    /// </summary>
    public enum UserStatus
    {
        Active,
        System,
        Blocked
    }
}