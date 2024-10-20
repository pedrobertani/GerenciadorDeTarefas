﻿using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class UserTaskDto
{
    public int TaskId { get; set; }
    public string Title { get; set; }

    public string Description { get; set; }

    public bool Completed { get; set; } = false;

    public DateTime DateCreated { get; set; } = DateTime.Now;

    public int UserId { get; set; }
    public User User { get; set; }
}
