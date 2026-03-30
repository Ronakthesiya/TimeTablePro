
export class CallAPI{
    static get(ajaxModel){
        $.ajax({
            url: ajaxModel.url,
            type: ajaxModel.type,
            headers: ajaxModel.headers,
            success: ajaxModel.success,
            error: ajaxModel.error
        })
    }

    static post(ajaxPostModel){

        console.log(ajaxPostModel);
        $.ajax({
            url: ajaxPostModel.url,
            type: ajaxPostModel.type,
            headers: ajaxPostModel.headers,
            data: ajaxPostModel.data,
            success: ajaxPostModel.success,
            error: ajaxPostModel.error
        })
    }


}