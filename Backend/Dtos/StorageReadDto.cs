﻿namespace Backend.Dtos
{
    public class StorageReadDto
    {
        public int StorageId { get; set; }
        public int UserID { get; set; } //temporary, need to change to proper UserID
        public string StorageName { get; set; }
    }
}