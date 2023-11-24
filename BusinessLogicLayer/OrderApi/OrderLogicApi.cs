using BusinessLogicLayer.Models;
using DataAccessLayer;
using DataAccessLayer.DBContext;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.OrderApi
{
    public class OrderLogicApi : IOrderLogicApi
    {
        private readonly OSMDBContext _dbContext;

        public OrderLogicApi(OSMDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public long SaveOrderDetail(OrderDetailsModel model)
        {
            long orderId = 0;
            OrderDetails orderDetails = new OrderDetails();
            if (model != null)
            {
                orderDetails.AddressId = model.AddressId;
                orderDetails.UserId = model.UserId;
                orderDetails.UpdatedOn = DateTime.UtcNow;
                orderDetails.CreatedOn = DateTime.UtcNow;
                orderDetails.CreatedBy = model.UserId;
                orderDetails.UpdatedBy = model.UserId;
                orderDetails.IsActive = model.IsActive;
                orderDetails.IsTermsAccepted = model.IsTermsAccepted;
                orderDetails.ShippingCharge = model.ShippingCharge;
                orderDetails.PaymentMode = model.PaymentMode;
                orderDetails.OrderNotes = model.OrderNotes;
                orderDetails.ProductAmount = model.ProductAmount;
                orderDetails.OrderStatus = model.OrderStatus;
                orderDetails.Tax = model.Tax;
                orderDetails.TotalAmount = model.TotalAmount;
                _dbContext.OrderDetails.Add(orderDetails);
                _dbContext.SaveChanges();
                orderId = orderDetails.OrderId;
            }
            return orderId;
        }

        public long SaveTransectionDetail(TransactionDetailsModel model)
        {
            long transectionId = 0;
            TransactionDetails transectionDetails = new TransactionDetails();
            if (model != null)
            {
                transectionDetails.IsPaymentSuccessful = model.IsPaymentSuccessful;
                transectionDetails.RefrenceId = model.RefrenceId;
                transectionDetails.OrderId = model.OrderId;
                transectionDetails.Amount = model.Amount;
                transectionDetails.GatewayName = model.GatewayName;
                transectionDetails.PayerId = model.PayerId;
                transectionDetails.PaymentId = model.PaymentId;
                transectionDetails.UpdatedOn = DateTime.UtcNow;
                transectionDetails.CreatedOn = DateTime.UtcNow;
                transectionDetails.CreatedBy = model.UserId;
                transectionDetails.UpdatedBy = model.UserId;
                _dbContext.TransactionDetails.Add(transectionDetails);
                _dbContext.SaveChanges();
                transectionId = transectionDetails.TransactionId;
            }
            return transectionId;
        }

        public bool SaveProductOrderRel(ProductOrderRelModel model)
        {
            bool status = false;
            ProductOrderRel pOrderRel = new ProductOrderRel();
            if (model != null)
            {
                pOrderRel.ProductId = model.ProductId;
                pOrderRel.Quantity = model.Quantity;
                pOrderRel.OrderId = model.OrderId;
                _dbContext.ProductOrderRel.Add(pOrderRel);
                _dbContext.SaveChanges();
                status = true;
            }
            return status;
        }

        public CheckoutSaveResponse SaveOrderDetails(CheckoutDetails checkoutDetails)
        {
            CheckoutSaveResponse checkoutSaveResponse = new CheckoutSaveResponse();
            if (checkoutDetails != null)
            {
                using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var order = new OrderDetails();
                        order.OrderStatus = "Successfull";
                        order.ShippingStatus = "Ready for shipping";
                        order.IsActive = true;
                        order.IsTermsAccepted = checkoutDetails.IsTermsAccepted;
                        order.OrderNotes = checkoutDetails.OrderNotes;
                        order.PaymentMode = checkoutDetails.PaymentMode;
                        order.TotalAmount = checkoutDetails.TotalOrderPrice;
                        order.UserId = checkoutDetails.UserId;
                        order.ProductAmount = checkoutDetails.TotalProductAmount;
                        order.ShippingCharge = checkoutDetails.TotalShipping;
                        order.Tax = checkoutDetails.TotalTax;
                        order.CreatedOn = DateTime.UtcNow;
                        order.CreatedBy = checkoutDetails.UserId; ;
                        order.UpdatedOn = DateTime.UtcNow;
                        order.UpdatedBy = checkoutDetails.UserId;
                        long addressId = 0;
                        if (checkoutDetails.IsShippingAnotherAddress)
                        {
                            var address = new AddressDetail();
                            address.FirstName = checkoutDetails.ShippingAddress.FirstName;
                            address.LastName = checkoutDetails.ShippingAddress.LastName;
                            address.Email = checkoutDetails.ShippingAddress.Email;
                            address.Mobile = checkoutDetails.ShippingAddress.Mobile;
                            address.Address = checkoutDetails.ShippingAddress.Address;
                            address.City = checkoutDetails.ShippingAddress.State;
                            address.Country = checkoutDetails.ShippingAddress.Country;
                            address.Pin = checkoutDetails.ShippingAddress.Pin;
                            address.IsActive = true;
                            address.CreatedOn = DateTime.UtcNow;
                            address.CreatedBy = checkoutDetails.UserId; ;
                            address.UpdatedOn = DateTime.UtcNow;
                            address.UpdatedBy = checkoutDetails.UserId;
                            _dbContext.AddressDetail.Add(address);
                            _dbContext.SaveChanges();
                            addressId = address.AddressId;
                        }
                        order.AddressId = addressId;
                        _dbContext.OrderDetails.Add(order);
                        _dbContext.SaveChanges();

                        if (checkoutDetails.BillingAddress != null)
                        {
                            var user = _dbContext.UserDetails.Where(m => m.UserId == checkoutDetails.UserId).FirstOrDefault();
                            if (user != null)
                            {
                                user.FirstName = checkoutDetails.BillingAddress.FirstName;
                                user.LastName = checkoutDetails.BillingAddress.LastName;
                                user.Email = checkoutDetails.BillingAddress.Email;
                                user.Mobile = checkoutDetails.BillingAddress.Mobile;
                                user.Address = checkoutDetails.BillingAddress.Address;
                                user.City = checkoutDetails.BillingAddress.State;
                                user.Country = checkoutDetails.BillingAddress.Country;
                                user.Pin = checkoutDetails.BillingAddress.Pin;
                                user.IsActive = true;
                                user.UpdatedOn = DateTime.UtcNow;
                                user.UpdatedBy = checkoutDetails.UserId;
                                _dbContext.Entry(user).State = EntityState.Modified;
                                _dbContext.SaveChanges();
                            }
                        }

                        foreach (var prod in checkoutDetails.Products)
                        {
                            var pOrderRel = new ProductOrderRel();
                            pOrderRel.OrderId = order.OrderId;
                            pOrderRel.ProductId = prod.ProductId;
                            pOrderRel.Quantity = Convert.ToInt32(prod.SelectedProductQuantity);
                            _dbContext.ProductOrderRel.Add(pOrderRel);
                            _dbContext.SaveChanges();
                        }

                        var transectionDetail = new TransactionDetails();
                        transectionDetail.IsPaymentSuccessful = false;
                        transectionDetail.GatewayName = "";
                        transectionDetail.PayerId = "";
                        transectionDetail.RefrenceId = "";
                        transectionDetail.PaymentId = "";
                        transectionDetail.Amount = checkoutDetails.TotalOrderPrice;
                        transectionDetail.OrderId = order.OrderId;
                        transectionDetail.CreatedOn = DateTime.UtcNow;
                        transectionDetail.CreatedBy = checkoutDetails.UserId; ;
                        transectionDetail.UpdatedOn = DateTime.UtcNow;
                        transectionDetail.UpdatedBy = checkoutDetails.UserId;
                        _dbContext.TransactionDetails.Add(transectionDetail);
                        _dbContext.SaveChanges();

                        transaction.Commit();

                        checkoutSaveResponse.OrderId = order.OrderId;
                        checkoutSaveResponse.IsSuccess = true;
                        checkoutSaveResponse.IsPaymentSuccessful = false;
                        checkoutSaveResponse.PaymentMode = order.PaymentMode;
                        checkoutSaveResponse.TransactionId = transectionDetail.TransactionId;
                        checkoutSaveResponse.OrderStatus = order.OrderStatus;
                        checkoutSaveResponse.ShippingStatus = order.ShippingStatus;
                        checkoutSaveResponse.TotalOrderPrice = order.TotalAmount;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        checkoutSaveResponse.IsSuccess = false;
                    }
                }

            }

            return checkoutSaveResponse;
        }

        public bool UpdateTransectionDetail(TransactionDetailsModel model)
        {
            bool status = false;
            var transection = _dbContext.TransactionDetails.Where(m => m.TransactionId == model.TransactionId).FirstOrDefault();
            if (transection != null)
            {
                transection.GatewayName = model.GatewayName;
                transection.IsPaymentSuccessful = model.IsPaymentSuccessful;
                transection.PayerId = model.PayerId;
                transection.PaymentId = model.PaymentId;
                transection.RefrenceId = model.RefrenceId;
                _dbContext.Entry(transection).State = EntityState.Modified;
                _dbContext.SaveChanges();
                status = true;
            }
            return status;
        }

        public bool UpdateOrderStatus(long orderId, string orderStatus)
        {
            bool status = false;
            var orderDetail = _dbContext.OrderDetails.Where(m => m.OrderId == orderId).FirstOrDefault();
            if (orderDetail != null)
            {
                orderDetail.OrderStatus = orderStatus;
                orderDetail.ShippingStatus = "Not Started";
                _dbContext.Entry(orderDetail).State = EntityState.Modified;
                _dbContext.SaveChanges();
                status = true;
            }
            return status;
        }

    }
}