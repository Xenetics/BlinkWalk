#ifdef FILESTORE_EXPORT
#define FILESTORE_API __declspec(dllexport)
#else
#define FILESTORE_API __declspec(dllimport)
#endif

#include "was/storage_account.h"
#include "was/blob.h"

extern "C"
{
	///<summary> Used for authentication with Azure </summary>
	utility::string_t m_ConnectionString;

	///<summary> Current Blob Store Connected to </summary>
	azure::storage::cloud_blob_client m_BlobClient;

	///<summary>  </summary>
	FILESTORE_API bool Connect(char* account_name, char* key);

	///<summary>  </summary>
	FILESTORE_API bool FolderCreate(char* folder_name);

	///<summary>  </summary>
	FILESTORE_API bool FolderDelete(char* folder_name);

	///<summary>  </summary>
	FILESTORE_API char* FolderList();

	///<summary>  </summary>
	FILESTORE_API bool FileCreate(char* folder_name, char* file_name, char* data);

	///<summary>  </summary>
	FILESTORE_API bool FileDelete(char* folder_name, char* file_name);

	///<summary>  </summary>
	FILESTORE_API char* FileList(char* folder_name);

	///<summary>  </summary>
	FILESTORE_API char* FileRetrieve(char* folder_name, char* file_name);
}