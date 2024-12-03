using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrMnager_mvc.Models;
using HrMnager_mvc.Models.RequestModels;
using HrMnager_mvc.Models.ResponseModel;
using HrManagerDetailResponse = HrMnager_mvc.Models.ResponseModel.HrManagerDetailResponse;

namespace HrMnager_mvc.Services.Interfaces
{
    public interface IHrManagerService
    {
        HrManagerResponse AddHrManager(CreateHrManagerRequest request, int departmentId);
        HrManagerResponse UpdateHrManager(UpdateHrManagerRequest request, int departmentId);
        HrManagerDetailResponse GetHrManagerById(int id);
        HrManagerDetailResponse GetHrManagerByEmail(string email);
        HrManagerListResponse GetAllHrManagers();
        BaseResponse Delete(int id);
    }
}