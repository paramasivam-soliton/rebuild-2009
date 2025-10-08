// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.DataAccessLayer.ProfileAdapter
// Assembly: PM.UserProfileManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 210CE7C9-6056-4099-A07B-EB2D78618349
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.dll

using PathMedical.DatabaseManagement;
using PathMedical.DatabaseManagement.Adapter;

#nullable disable
namespace PathMedical.UserProfileManagement.DataAccessLayer;

public class ProfileAdapter(DBScope scope) : AdapterBase<UserProfile>(scope)
{
}
