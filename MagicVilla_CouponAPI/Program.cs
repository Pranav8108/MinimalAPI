using AutoMapper;
using FluentValidation;
using MagicVilla_CouponAPI;
using MagicVilla_CouponAPI.Data;
using MagicVilla_CouponAPI.Models;
using MagicVilla_CouponAPI.Models.dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.MapGet("/Hello GET", () => "Hello world");   //this creates the get method with hello world and the 2nd hello world is the output
//app.MapGet("/Hello Get{id:int}", (int id) =>      //for complex logic we do like this
//{
//    return Results.Ok("ID: "+id);
//});
//app.MapPost("/Hello POST", () => Results.Ok("Hello world post"));

app.MapGet("/api/coupon", (ILogger <Program> _logger) => {
    APIresponse response = new();
    _logger.Log(LogLevel.Information, "Getting all the coupons");
    response.result = CouponStore.couponList;
    response.StatusCode = HttpStatusCode.OK;
    response.IsSuccess = true;
   return Results.Ok(response);
    }).WithName("GetCoupons").Produces<APIresponse>(200);







app.MapGet("/api/coupon/{id:int}", (int id) => {
    APIresponse response = new(); 
    response.result = (CouponStore.couponList.FirstOrDefault(x => x.Id == id));
    response.StatusCode = HttpStatusCode.OK;
    response.IsSuccess = true;
    return Results.Ok(response); 
}).WithName("GetCoupon").Produces<APIresponse>(200);









app.MapPost("/api/coupon",async (IValidator <CouposDto> _validator,IMapper _mapper,[FromBody] CouposDto  coupondto) => //imapper from nuget
{
    APIresponse response = new() { IsSuccess = false , StatusCode=HttpStatusCode.BadRequest};
    var validationResult = await _validator.ValidateAsync (coupondto);
    if ( !validationResult.IsValid)
    {
        response.ErrorMessages.Add(validationResult.Errors.FirstOrDefault().ToString());
        return Results.BadRequest(response);
    }
    if (CouponStore.couponList.FirstOrDefault(x => x.Name.ToLower() == coupondto.Name.ToLower()) != null)
    {

        response.ErrorMessages.Add("already exist");
        return Results.BadRequest(response);
        
    }
    Coupon coupon = _mapper.Map < Coupon >(coupondto); // yo naya post maa k k matra input dinu parcha tesko lagi ..Accepts<couposdto>
    //{                                                  //coupondto ko object Coupon maa halne
    //    Name = coupondto.Name,
    //    IsActive = coupondto.IsActive,
    //    Pecentage = coupondto.Pecentage,
    //};
    coupon.Id = CouponStore.couponList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
    CouponStore.couponList.Add(coupon);
    CCoupondto ccoupton = _mapper.Map<CCoupondto>(coupon); //yo display garda k k dekhaune tesko lagi...produces<ccoupondto>
                                                          
    //{
                                                           //    Id = coupon.Id,
                                                           //    Name = coupon.Name,
                                                           //    IsActive = coupon.IsActive,
                                                           //    Pecentage = coupon.Pecentage,
                                                           //    Created = coupon.Created
                                                           //};
                                                           // return Results.CreatedAtRoute("GetCoupon", new { id = coupon.Id }, ccoupton);

         response.result = ccoupton;
         response.StatusCode = HttpStatusCode.OK;
         response.IsSuccess = true;
         return Results.Ok(response);

}).WithName("CreatedCoupons").Accepts<CouposDto>("application/json").Produces<APIresponse>(201).Produces(400);







app.MapPut("/api/coupon", async (IValidator<UpdateDto> _validator, IMapper _mapper, [FromBody] UpdateDto updatedto) =>
{
    APIresponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
    var validationResult = await _validator.ValidateAsync(updatedto);
    if (!validationResult.IsValid)
    {
        response.ErrorMessages.Add(validationResult.Errors.FirstOrDefault().ToString());
        return Results.BadRequest(response);
    }
    Coupon couponFromStore = CouponStore.couponList.FirstOrDefault(x => x.Id == updatedto.Id);//to validate we need to retrieve the coupon
    couponFromStore.IsActive = updatedto.IsActive;
    couponFromStore.Name = updatedto.Name;
    couponFromStore.Pecentage = updatedto.Pecentage;
    response.result = _mapper.Map<UpdateDto>(couponFromStore); ;
    response.StatusCode = HttpStatusCode.OK;
    response.IsSuccess = true;
    return Results.Ok(response);
}).WithName("updateCoupons").Accepts<UpdateDto>("application/json").Produces<APIresponse>(201);






app.MapDelete("/api/coupon{id:int}", (int id) =>
{
    APIresponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
  
    Coupon couponFromStore = CouponStore.couponList.FirstOrDefault(x => x.Id == id);//to validate we need to retrieve the coupon
    if (couponFromStore != null) 
    {
    CouponStore.couponList.Remove(couponFromStore);
    response.StatusCode = HttpStatusCode.OK;
    response.IsSuccess = true;
    return Results.Ok(response);
    }
    else
    {
        response.ErrorMessages.Add("invalid id");
        return Results.BadRequest(response);
    }
});


app.UseHttpsRedirection();

app.Run();
