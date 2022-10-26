using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Kidz.Queries;
using Kidz.DatabaseConnection;
using Kidz.Constants;
using Kidz.Function;
using Kidz.Controllers;

namespace Kidz.Models;

[ApiController]
public class UserController : BaseController
{
    string hostUrl = WebAddressConstant.STRING_SCHME_HTTP + WebAddressConstant.STRING_SCHME_LOCALHOST + WebAddressConstant.STRING_PORT_KIDZ;
    public UserController(DatabaseContex databaseContex) : base(databaseContex)
    {

    }

    [HttpPost]
    [Route("[controller]/selectUserLogin")]
    public ResponseModel selectUserLogin([FromBody] String stringRequest)
    {
        UserModel modelUser = new UserModel();
        ResponseModel modelResponse = new ResponseModel();
        HitHistoryModel modelHitHistory = new HitHistoryModel();
        ResultHistoryModel modelResultHistory = new ResultHistoryModel();
        RequestModel modelRequest = new RequestModel();
        String stringRequestDecoded = "";
        bool boolException = false;
        try
        {
            stringRequestDecoded = base64Decode(stringRequest);
        }
        catch (Exception exception)
        {
            modelResponse.MessageContent = exception.Message;
            modelResponse.MessageTitle = StringConstant.STRING_MESSAGE_TITLE_FAIL;
            modelRequest.Data = exception.Message;
            modelResultHistory.Data = exception.Message;
            boolException = true;
            modelHitHistory.setHitHistoryFromRequest(modelRequest,hostUrl+"/selectUserLogin");
            _hitHistoryQuery.insertHistory(modelHitHistory);
        }




        if(!boolException)
        {
            if(!(string.IsNullOrEmpty(stringRequestDecoded)))
            {
                modelRequest = JsonSerializer.Deserialize<RequestModel>(stringRequestDecoded);
                modelUser = JsonSerializer.Deserialize<UserModel>(modelRequest.Data);
                UserModel modelUserFromDb = _userQuery.selectUser(modelUser);

                //check user on db
                if (modelUserFromDb != null)
                {
                    //insert token to db
                    Random intRandom = new Random();
                    modelUserFromDb.Token = intRandom.Next(111,222).ToString();
                    modelResponse = _userQuery.insertUserToken(modelUserFromDb);
                }
                else
                {
                    modelResponse.ServiceResponseCode = ServiceResponseCodeConstant.STRING_RESPONSECODE_MODULE_FAIL;
                    modelResponse.MessageContent = StringConstant.STRING_MESSAGE_CONTENT_USER_NOTFOUND;
                    modelResponse.MessageTitle = StringConstant.STRING_MESSAGE_TITLE_FAIL;
                }
            }
            else
            {
                modelResponse.ServiceResponseCode = ServiceResponseCodeConstant.STRING_RESPONSECODE_MODULE_SUCCESS;
                modelResponse.MessageContent = StringConstant.STRING_MESSAGE_CONTENT_JSONSERIALIZE_FAIL;
                modelResponse.MessageTitle = StringConstant.STRING_MESSAGE_TITLE_FAIL;
            }
        }

        modelResultHistory.setResultHistoryModel(modelResponse);
        _resultHistoryQuery.insertResultHistory(modelResultHistory);

        return modelResponse;
    }

    [HttpPost]
    [Route("[controller]/insertUser")]
    public ResponseModel insertUser([FromBody] String stringRequest)
    {
        UserModel modelUser = new UserModel();
        ResponseModel modelResponse = new ResponseModel();
        RequestModel modelRequest = new RequestModel();
        HitHistoryModel modelHitHistory = new HitHistoryModel();
        ResultHistoryModel modelResultHistory = new ResultHistoryModel();
        String stringRequestDecoded = "";
        bool boolException = false;

        modelResponse.RequestedBy = modelResponse.RequestedBy;
        modelRequest.CreatedOn = DateTime.Now;

        try
        {
            stringRequestDecoded = base64Decode(stringRequest);
        }
        catch (Exception exception)
        {
            modelResponse.MessageContent = exception.Message;
            modelResponse.MessageTitle = StringConstant.STRING_MESSAGE_TITLE_FAIL;
            modelRequest.Data = exception.Message;
            boolException = true;
            modelHitHistory.setHitHistoryFromRequest(modelRequest,hostUrl+"/insertUser");
            _hitHistoryQuery.insertHistory(modelHitHistory);
            modelResultHistory.Data = exception.Message;
        }

        if(!boolException)
        {
            if(!(string.IsNullOrEmpty(stringRequestDecoded)))
            {
                //url attribute = convert from route
                modelRequest = JsonSerializer.Deserialize<RequestModel>(stringRequestDecoded);
                modelHitHistory.setHitHistoryFromRequest(modelRequest,hostUrl+"/insertUser");
                _hitHistoryQuery.insertHistory(modelHitHistory);
                modelUser = JsonSerializer.Deserialize<UserModel>(modelRequest.Data);
                //return response model
                if(modelUser.validateUser())
                {
                    modelResponse = _userQuery.insertUser(modelUser);
                }
            }
                else
            {
                modelResponse.ServiceResponseCode = ServiceResponseCodeConstant.STRING_RESPONSECODE_MODULE_SUCCESS;
                modelResponse.MessageContent = StringConstant.STRING_MESSAGE_CONTENT_JSONSERIALIZE_FAIL;
                modelResponse.MessageTitle = StringConstant.STRING_MESSAGE_TITLE_FAIL;
            }

        }

        modelResultHistory.setResultHistoryModel(modelResponse);
        _resultHistoryQuery.insertResultHistory(modelResultHistory);

       return modelResponse;
    }

    [HttpPost]
    [Route("[controller]/selectUserLoginTest")]
    public UserModel selectrUserLoginTest([FromBody] UserModel modelUser)
    {
        UserModel modelUserResponse = new UserModel();

        if(modelUser.validateUser())
        {
            // UserModel? modelUserResult = _userQuery.entityUser.Where(user => user.Username == modelUser.Username && user.Password == modelUser.Password).SingleOrDefault<UserModel>();
            // modelUserResponse = modelUserResult;
        }
       return modelUserResponse;
    }

    [HttpPost]
    [Route("[controller]/updateUser")]
    public UserModel updateUser([FromBody] UserModel modelUserUpdate)
    {
        UserModel modelUserResponse = new UserModel();

        if( modelUserUpdate.validateUser())
        {
            // UserModel? modelUserResult = Contex.entityUser.Where(s => s.ID == modelUserUpdate.ID).  SingleOrDefault<UserModel>();

            // modelUserResult.Username = modelUserUpdate.Username;
            // modelUserResult.Password = modelUserUpdate.Password;
            // modelUserResult.ID = modelUserResult.ID;
            // modelUserResult.DeviceId = modelUserUpdate.DeviceId;

            // Contex.entityUser.Update(modelUserResult);
            // Contex.SaveChanges();

            // modelUserResponse = modelUserResult;

        }
       return modelUserResponse;
    }

    [HttpPost]
    [Route("[controller]/calculatePersegi")]
    public ResponseModel calculatePersegi([FromBody] RequestModel modelRequest)
    {
        UserModel modelUser = new UserModel();
        ResponseModel modelResponse = new ResponseModel();
        HitHistoryModel modelHitHistory = new HitHistoryModel();
        ResultHistoryModel modelResultHistory = new ResultHistoryModel();
        RequestHistoryModel modelRequestHistory = new RequestHistoryModel();
        ResponseHistoryModel modelResponseHistory = new ResponseHistoryModel();

        modelResponse.RequestedBy =modelResponse.RequestedBy;
        modelRequest.CreatedOn = DateTime.Now;

        //url attribute = convert from route
        modelHitHistory.setHitHistoryFromRequest(modelRequest,hostUrl+"/calculatePersegi");
        _hitHistoryQuery.insertHistory(modelHitHistory);

        string stringData = base64Decode(modelRequest.Data);

        if(!(string.IsNullOrEmpty(stringData)))
        {
            modelUser = JsonSerializer.Deserialize<UserModel>(stringData);

            if(modelUser.Token != null)
            {
                if(modelUser.modelPersegi != null)
                {
                    string stringURL_CALCULATEPERSEGI = WebAddressConstant.STRING_SCHME_HTTPS + WebAddressConstant.STRING_SCHME_LOCALHOST + WebAddressConstant.STRING_PORT_BANGUNRUANG + WebAddressConstant.STRING_BANGUNRUANG_CALCULATEPERSEGI;

                    modelRequestHistory.setRequestHistoryModel(modelRequest, stringURL_CALCULATEPERSEGI);
                    _requestHistoryQuery.insertRequestHistory(modelRequestHistory);

                    modelResponse = callInternalService(stringURL_CALCULATEPERSEGI, modelRequest, modelUser.Token);

                    modelResponseHistory.setResponseHistoryModel(modelResponse);
                    _responseHistoryQuery.insertResponseHistory(modelResponseHistory);
                }
                else
                {
                    modelResponse.ServiceResponseCode = ServiceResponseCodeConstant.STRING_RESPONSECODE_MODULE_FAIL;
                    modelResponse.MessageContent = StringConstant.STRING_MESSAGE_CONTENT_FAIL;
                    modelResponse.MessageTitle = StringConstant.STRING_MESSAGE_TITLE_FAIL;
                }
            }
            else
            {
                modelResponse.ServiceResponseCode = ServiceResponseCodeConstant.STRING_RESPONSECODE_MODULE_FAIL;
                modelResponse.MessageContent = StringConstant.STRING_MESSAGE_CONTENT_ACCESS_DENY;
                modelResponse.MessageTitle = StringConstant.STRING_MESSAGE_TITLE_FAIL;
            }

        }
        else
        {
            modelResponse.ServiceResponseCode = ServiceResponseCodeConstant.STRING_RESPONSECODE_MODULE_SUCCESS;
            modelResponse.MessageContent = StringConstant.STRING_MESSAGE_CONTENT_JSONSERIALIZE_FAIL;
            modelResponse.MessageTitle = StringConstant.STRING_MESSAGE_TITLE_FAIL;
        }

         modelResultHistory.setResultHistoryModel(modelResponse);
        _resultHistoryQuery.insertResultHistory(modelResultHistory);
        return modelResponse;

    }
}