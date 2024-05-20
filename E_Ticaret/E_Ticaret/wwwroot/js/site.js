function viewStatus(type, text) {
	if (type) {
		Swal.fire({
			icon: "success",
			type: "success",
			title: text,
			timer: 1500
		})
	} else {
		Swal.fire({
			icon: "error",
			type: "error",
			title: text,
			confirmButtonColor: "#556ee6"
		})
	}
}

function formatNumber(number) {
    return number.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
}

function cartUpdate() {
    $.ajax({
        url: "/Cart/Get",
        method: "GET",
        dataType: "JSON",
        success: function (data) {
            var carts = data.carts;
            var get_count = carts.length;
            var tableBody = $('#TableBody');

            $(".cart_totalg").html(formatNumber(parseFloat(data.total)) + ' ₺');
            $(".cart_count").html(data.count);
            $(".cart_total").html(formatNumber(data.total) + " ₺");

            if (data.cTotal != 0) {
                $("#cart_c").css('display', 'block');
                $("#cart_cs").css('display', 'block');
                $("#cart_cc").css('display', 'block');
                $(".cart_totalc").html('- ' + formatNumber(parseFloat(data.cTotal)) + ' ₺');

                $(".cart_totalp").html(formatNumber(parseFloat(data.total - data.cTotal)) + ' ₺');
                $(".cart_total").html(formatNumber(parseFloat(data.total - data.cTotal)) + ' ₺');

                var tacss = "";

                if (data.coupon.coupon.type == 1) {
                    tacss = " ( - " + data.coupon.coupon.discountAmount + " ₺ )";
                }

                if (data.coupon.coupon.type == 2) {
                    tacss = " ( - %" + data.coupon.coupon.discountAmount + " )";
                }

                $('.del_coupon').remove();

                var closeCell = $('#coupon_li');
                var button = $('<span class="del_coupon" onclick="DeleteCoupon(' + data.coupon.couponHistoryId + ')">x</span>');
                closeCell.append(button);

                $("#coupon_name").html(data.coupon.coupon.name + tacss);
            } else {
                $("#cart_c").css('display', 'none');
                $("#cart_cs").css('display', 'none');
                $("#cart_cc").css('display', 'none');
                $(".cart_totalc").html("");

                $(".cart_totalp").html(formatNumber(parseFloat(data.total)) + ' ₺');
                $(".cart_total").html(formatNumber(parseFloat(data.total)) + ' ₺');

                $("#coupon_name").html("");

                $('.del_coupon').remove();
            }

            tableBody.empty(); // Tabloyu temizle

            for (var i = 0; i < get_count; i++) {
                var cart = carts[i];

                var tableRow = $('<tr>');

                var productCell = $('<td>').addClass('product__cart__item');
                productCell.append($('<div>').addClass('product__cart__item__pic')
                    .append($('<img>').addClass('card-img-top').attr('src', 'img/' + cart.product.pictures[0].path).attr('alt', cart.product.name).css('height', '90px'))
                );
                productCell.append($('<div>').addClass('product__cart__item__text')
                    .append($('<h6>').text(cart.product.name))
                    .append($('<h5>').text(formatNumber(parseFloat(cart.product.price)) + ' ₺'))
                );
                var quantityCell = $('<td>').addClass('quantity__item');
                var quantityDiv = $('<div>').addClass('quantity');
                var proQtyDiv = $('<div>').addClass('pro-qty-2');
                var quantityInput = $('<input>').attr({ type: 'text', class: 'cartQuantity', action: 'Cart/Edit/' + cart.cartId, name: 'Quantity', id: cart.cartId, value: cart.quantity });
                proQtyDiv.append(quantityInput);
                quantityDiv.append(proQtyDiv);
                quantityCell.append(quantityDiv);

                var priceCell = $('<td>').addClass('cart__price').text(formatNumber(parseFloat(cart.total)) + ' ₺');

                var closeCell = $('<td>').addClass('cart__close');
                var button = $('<button onclick="DeleteCart(' + cart.cartId + ')">').attr('id', cart.cartId).css({ border: 'unset', background: 'unset' }).append($('<i>').addClass('fa fa-close'));
                closeCell.append(button);

                tableRow.append(productCell, quantityCell, priceCell, closeCell);
                tableBody.append(tableRow);
            }

            var proQty = $('.pro-qty');
            proQty.prepend('<span class="fa fa-angle-up dec qtybtn"></span>');
            proQty.append('<span class="fa fa-angle-down inc qtybtn"></span>');
            proQty.on('click', '.qtybtn', function () {
                var $button = $(this);
                var oldValue = $button.parent().find('input').val();
                var input_acc = $button.parent().find('input').attr("action");
                if ($button.hasClass('inc')) {
                    var newVal = parseFloat(oldValue) + 1;
                } else {
                    if (oldValue >= 1) {
                        var newVal = parseFloat(oldValue) - 1;
                    } else {
                        newVal = 1;
                    }
                }
                $button.parent().find('input').val(newVal).trigger('change');
            });

            var proQty = $('.pro-qty-2');
            proQty.prepend('<span class="fa fa-angle-left dec qtybtn"></span>');
            proQty.append('<span class="fa fa-angle-right inc qtybtn"></span>');
            proQty.on('click', '.qtybtn', function () {
                var $button = $(this);
                var oldValue = $button.parent().find('input').val();
                if ($button.hasClass('inc')) {
                    var newVal = parseFloat(oldValue) + 1;
                } else {
                    // Don't allow decrementing below zero
                    if (oldValue > 1) {
                        var newVal = parseFloat(oldValue) - 1;
                    } else {
                        newVal = 1;
                    }
                }
                $button.parent().find('input').val(newVal).trigger('change');
            });
        }
    });
}

function DeleteCart(id) {
    Swal.fire({
        title: 'Silmek Emin misiniz ?',
        text: "Evete bastığınızda ürün sepetten silinecektir!",
        icon: 'warning',
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Evet, Sil!',
        showCancelButton: true,
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "Cart/Delete/" + id,
                method: "POST",
                success: function (data) {
                    cartUpdate();
                }
            });
        }
    });
}

function DeleteCoupon(id) {
    Swal.fire({
        title: 'Silmek Emin misiniz ?',
        text: "Evete bastığınızda kupon silinecektir!",
        icon: 'warning',
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Evet, Sil!',
        showCancelButton: true,
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "Cart/Coupon/" + id,
                method: "POST",
                dataType: "JSON",
                success: function (data) {
                    cartUpdate();
                    viewStatus(data.status, data.msg);
                }
            });
        }
    });
}

cartUpdate();

$(document).on('change', '.cartQuantity', function (e) {
    var objc = $(this);

    var quantity = objc.val();

    $.ajax({
        url: objc.attr("action"),
        method: "POST",
        dataType: "JSON",
        data: {
            Quantity: quantity
        },
        success: function (data) {
            cartUpdate();
        },
        error: function (xhr, status, error) {
            viewStatus(false, "Sunucu Hatası");
            form_btn.attr('disabled', false);
        }
    });
});

$(".form_send").submit(function (e) {
    var form = $(this);
    var formData = new FormData(form[0]);

    var form_btn = form.find('button');
    form_btn.attr('disabled', true);

    Swal.fire("İşlem Gerçekleştiriliyor!", "", "warning");

    e.preventDefault();

    if (localStorage.getItem("token")) {
        const token = JSON.parse(localStorage.getItem("token"));

        $.ajax({
            url: $(this).attr("action"),
            method: "POST",
            dataType: "JSON",
            data: formData,
            contentType: false,
            processData: false,
            success: function (data) {
                if ('token' in data) {
                    localStorage.setItem("token", JSON.stringify(data.token));
                }
                cartUpdate();
                viewStatus(data.status, data.msg);
                form_btn.attr('disabled', false);
            },
            error: function (xhr, status, error) {
                viewStatus(false, "Sunucu Hatası");
                form_btn.attr('disabled', false);
            }
        });
    } else {
        $.ajax({
            url: $(this).attr("action"),
            method: "POST",
            dataType: "JSON",
            data: formData,
            contentType: false,
            processData: false,
            success: function (data) {
                if ('token' in data) {
                    localStorage.setItem("token", JSON.stringify(data.token));
                }
                cartUpdate();
                viewStatus(data.status, data.msg);
                form_btn.attr('disabled', false);
            },
            error: function (xhr, status, error) {
                viewStatus(false, "Sunucu Hatası");
                form_btn.attr('disabled', false);
            }
        });
    }
});

$("form").submit(function (e) {
    var form = $(this);
    var formData = new FormData(form[0]);

    var formDataObject = {};
    formData.forEach(function (value, key) {
        formDataObject[key] = value;
    });

    var form_btn = form.find('button');
    form_btn.attr('disabled', true);

    Swal.fire("İşlem Gerçekleştiriliyor!", "", "warning");

    e.preventDefault();

    if (localStorage.getItem("token")) {
        const token = JSON.parse(localStorage.getItem("token"));

        $.ajax({
            url: $(this).attr("action"),
            method: "POST",
            dataType: "JSON",
            data: formDataObject,
            success: function (data) {
                if ('token' in data) {
                    localStorage.setItem("token", JSON.stringify(data.token));
                }
                cartUpdate();
                viewStatus(data.status, data.msg);
                form_btn.attr('disabled', false);
            },
            error: function (xhr, status, error) {
                viewStatus(false, "Sunucu Hatası");
                form_btn.attr('disabled', false);
            }
        });
    } else {
        $.ajax({
            url: $(this).attr("action"),
            method: "POST",
            dataType: "JSON",
            data: formDataObject,
            success: function (data) {
                if ('token' in data) {
                    localStorage.setItem("token", JSON.stringify(data.token));
                }
                cartUpdate();
                viewStatus(data.status, data.msg);
                form_btn.attr('disabled', false);
            },
            error: function (xhr, status, error) {
                viewStatus(false, "Sunucu Hatası");
                form_btn.attr('disabled', false);
            }
        });
    }
});