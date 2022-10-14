using Kidz.Constants;
using Kidz.DatabaseConnection;
using Kidz.Models;

namespace Kidz.Queries
{
    public class UserQuery
    {
        private DatabaseContex _databaseContex;

        public UserQuery(DatabaseContex databaseContex)
        {
            this._databaseContex = databaseContex;
        }

        public ResponseModel insertUser(UserModel modelUser)
        {
            ResponseModel modelResponse = new ResponseModel();


            UserModel modelUserResult = selectUser(modelUser);

            if(modelUserResult == null)
            {
                _databaseContex.entityUser.Add(modelUser);

                if(_databaseContex.SaveChanges() > 0)
                {
                    modelResponse.ServiceResponseCode = ServiceResponseCodeConstant.STRING_RESPONSECODE_MODULE_SUCCESS;
                    modelResponse.MessageContent = StringConstant.STRING_MESSAGE_CONTENT_SUCCESS;
                    modelResponse.MessageTitle = StringConstant.STRING_MESSAGE_TITLE_SUCCESS;

                }
                else
                {
                    modelResponse.ServiceResponseCode = ServiceResponseCodeConstant.STRING_RESPONSECODE_MODULE_FAIL;
                    modelResponse.MessageContent = StringConstant.STRING_MESSAGE_CONTENT_INSERTFAIL;
                    modelResponse.MessageTitle = StringConstant.STRING_MESSAGE_TITLE_FAIL;
                }
            }
            else
            {
                modelResponse.ServiceResponseCode = ServiceResponseCodeConstant.STRING_RESPONSECODE_MODULE_FAIL;
                modelResponse.MessageContent = StringConstant.STRING_MESSAGE_CONTENT_USERDUPLICATE;
                modelResponse.MessageTitle = StringConstant.STRING_MESSAGE_TITLE_FAIL;
            }

            return modelResponse;
        }

        public UserModel selectUser (UserModel modelUser)
        {
            UserModel responseModelUser = _databaseContex.entityUser.Where(user => user.Username == modelUser.Username && user.Password == modelUser.Password).FirstOrDefault<UserModel>();

            return responseModelUser;
        }

        public ResponseModel insertUserToken (UserModel modelUser)
        {
            ResponseModel modelResponse = new ResponseModel();
            UserModel modelUserResult = selectUser(modelUser);
            modelUserResult.Token = modelUser.Token;
            _databaseContex.entityUser.Update(modelUserResult);

            if(_databaseContex.SaveChanges() > 0)
            {
                modelResponse.ServiceResponseCode = ServiceResponseCodeConstant.STRING_RESPONSECODE_MODULE_SUCCESS;
                modelResponse.MessageContent = StringConstant.STRING_MESSAGE_CONTENT_SUCCESS;
                modelResponse.MessageTitle = StringConstant.STRING_MESSAGE_TITLE_SUCCESS;

            }
            else
            {
                modelResponse.ServiceResponseCode = ServiceResponseCodeConstant.STRING_RESPONSECODE_MODULE_FAIL;
                modelResponse.MessageContent = StringConstant.STRING_MESSAGE_CONTENT_INSERTFAIL;
                modelResponse.MessageTitle = StringConstant.STRING_MESSAGE_TITLE_FAIL;
            }

            return modelResponse;
        }

        public UserModel selectUserByToken(UserModel modelUser)
        {
            return _databaseContex.entityUser.Where(user => modelUser.Token == user.Token).FirstOrDefault<UserModel>();
        }


    }
}