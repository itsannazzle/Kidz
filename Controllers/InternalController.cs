using System.Text;
using System.Text.Json;
using Kidz.Constants;
using Kidz.DatabaseConnection;
using Kidz.Models;
using Kidz.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Kidz.Controllers
{
    [ApiController]
    public class InternalController : ControllerBase
    {
        UserQuery _userQuery;
        HitHistoryQuery _hitHistoryQuery;
        ResultHistoryQuery _resultHistoryQuery;
        RequestHistoryQuery _requestHistoryQuery;
        ResponseHistoryQuery _responseHistoryQuery;

        string hostUrl = WebAddressConstant.STRING_SCHME_HTTPS + WebAddressConstant.STRING_SCHME_LOCALHOST + WebAddressConstant.STRING_PORT_BANGUNRUANG;

        public InternalController(DatabaseContex databaseContex)
        {
            _userQuery = new UserQuery(databaseContex);
            _hitHistoryQuery = new HitHistoryQuery(databaseContex);
            _resultHistoryQuery = new ResultHistoryQuery(databaseContex);
            _requestHistoryQuery = new RequestHistoryQuery(databaseContex);
            _responseHistoryQuery = new ResponseHistoryQuery(databaseContex);
        }

        [HttpPost]
        [Route("[controller]/validateUserToken")]
        public ResponseModel validateUserToken(RequestModel modelRequest)
        {
            ResponseModel modelResponse = new ResponseModel();
            UserModel modelUserData = JsonSerializer.Deserialize<UserModel>(modelRequest.Data);
            UserModel modelUser = _userQuery.selectUserByToken(modelUserData);

            if( modelUser != null)
            {
                modelResponse.ServiceResponseCode = ServiceResponseCodeConstant.STRING_RESPONSECODE_MODULE_SUCCESS;
                modelResponse.MessageContent = StringConstant.STRING_MESSAGE_CONTENT_SUCCESS;
                modelResponse.MessageTitle = StringConstant.STRING_MESSAGE_TITLE_SUCCESS;
                modelResponse.Data = JsonSerializer.Serialize(modelUser);
            }
            else
            {
                modelResponse.ServiceResponseCode = ServiceResponseCodeConstant.STRING_RESPONSECODE_MODULE_FAIL;
                modelResponse.MessageContent = StringConstant.STRING_MESSAGE_CONTENT_FAIL;
                modelResponse.MessageTitle = StringConstant.STRING_MESSAGE_TITLE_FAIL;
                modelResponse.Data = JsonSerializer.Serialize(modelUser);
            }

            return modelResponse;
        }
    }
}