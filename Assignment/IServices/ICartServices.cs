﻿using Assignment.Models;

namespace Assignment.IServices
{
    public interface ICartServices
    {
        public bool CreateCart(Cart p);
        public bool UpdateCart(Cart p);
        public bool DeleteCart(Guid id);
        public List<Cart> GetAllCart();
    }
}
