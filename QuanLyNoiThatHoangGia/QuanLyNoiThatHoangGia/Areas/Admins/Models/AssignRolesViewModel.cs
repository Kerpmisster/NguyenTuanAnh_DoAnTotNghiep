namespace QuanLyNoiThatHoangGia.Areas.Admins.Models
{
    public class AssignRolesViewModel
    {
        public string Id { get; set; }
        public List<string> AllRoles { get; set; } = new();
        public List<string> SelectedRoles { get; set; } = new();
    }
}
