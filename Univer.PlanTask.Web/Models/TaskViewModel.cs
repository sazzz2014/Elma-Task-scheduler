using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Univer.PlanTask.Web.Models
{
    public class Task
    {
        public virtual long Id { get; set; }

        [Display(Name = "Наименование")]
        [StringLength(50, MinimumLength = 7)]
        public virtual string Name { get; set; }

        [Display(Name = "Дата начала")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Нам нужно знать, когда начинать")]
        public virtual DateTime DateStart { get; set; }

        [Display(Name = "Дата окончания")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Без дедлайна эта задача не будет сделана")]
        public virtual DateTime DateEnd { get; set; }

        [EnumDataType(typeof(TaskStatus), ErrorMessage = "Изменить, если что-то сделано")]
        public virtual TaskStatus Status { get; set; }

        [ReadOnly(true)]
        public virtual bool IsSimple { get; set; }

        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        [UIHint("TextAlert")]
        public virtual string Description { get; set; }

        [Display(Name = "Бюджет")]
        [DataType(DataType.Currency)]
        public virtual double Money { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public virtual User User { get; set; }
    }

    public enum TaskStatus
    {
        New,
        Fail,
        Done
    }
}