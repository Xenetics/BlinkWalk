#include "LevelStore.h"
#include "was/storage_account.h"
#include "was/blob.h"

extern "C"
{
	bool Connect(char* account_name, char* key)
	{
		try
		{
			m_ConnectionString =	U("DefaultEndpointsProtocol=https;AccountName=") 
									+ utility::conversions::to_string_t(account_name) 
									+ U(";AccountKey=") 
									+ utility::conversions::to_string_t(key) 
									+ U(";");
			azure::storage::cloud_storage_account storageAccount = azure::storage::cloud_storage_account::parse(m_ConnectionString);
			m_BlobClient = storageAccount.create_cloud_blob_client();
			return true;
		}
		catch (const std::exception e)
		{
			std::wcout << U("Error: ") << e.what() << std::endl;
			return false;
		}
	}

	bool FolderCreate(char* folder_name) 
	{
		try
		{
			azure::storage::cloud_blob_container container = m_BlobClient.get_container_reference(utility::conversions::to_string_t(folder_name));
			container.create_if_not_exists();
			return true;
		}
		catch (const std::exception e)
		{
			std::wcout << U("Error: ") << e.what() << std::endl;
			return false;
		}
	}

	bool FolderDelete(char* folder_name)
	{
		try
		{
			azure::storage::cloud_blob_container container = m_BlobClient.get_container_reference(utility::conversions::to_string_t(folder_name));
			container.delete_container_if_exists();
		}
		catch (const std::exception e)
		{
			std::wcout << U("Error: ") << e.what() << std::endl;
			return false;
		}
	}

	char* FolderList()
	{
		char* contents;
		try
		{
			azure::storage::result_segment<azure::storage::cloud_blob_container> containers = m_BlobClient.list_containers_segmented(containers.continuation_token);
			int contentsSize = 0;
			for each(azure::storage::cloud_blob_container container in containers.results())
			{
				for (int i = 0; i < container.name().size(); ++i)
				{
					contentsSize++;
				}
				contentsSize++;
			}
			contents = new char[contentsSize];
			contents[contentsSize] = '\0';
			int i = 0;
			for each(azure::storage::cloud_blob_container container in containers.results())
			{
				for (int j = 0; j < container.name().size(); ++j)
				{
					contents[i] = container.name[j];
					i++;
				}
				contents[i] = '#';
				i++;
			}
			return contents;
		}
		catch (const std::exception e)
		{
			std::wcout << U("Error: ") << e.what() << std::endl;
			contents = new char[0];
			return contents;
		}
	}

	bool FileCreate(char* folder_name, char* file_name, char* data)
	{
		try
		{
			azure::storage::cloud_blob_container container = m_BlobClient.get_container_reference(utility::conversions::to_string_t(folder_name));
			azure::storage::cloud_block_blob blob = container.get_block_blob_reference(utility::conversions::to_string_t(file_name));
			blob.upload_text(utility::conversions::to_string_t(data));
			return true;
		}
		catch (const std::exception e)
		{
			std::wcout << U("Error: ") << e.what() << std::endl;
			return false;
		}
	}

	bool FileDelete(char* folder_name, char* file_name)
	{
		try
		{
			azure::storage::cloud_blob_container container = m_BlobClient.get_container_reference(utility::conversions::to_string_t(folder_name));
			azure::storage::cloud_block_blob blob = container.get_block_blob_reference(utility::conversions::to_string_t(file_name));
			blob.delete_blob();
		}
		catch (const std::exception e)
		{
			std::wcout << U("Error: ") << e.what() << std::endl;
			return false;
		}
	}

	char* FileList(char* folder_name)
	{
		char* files;
		try
		{
			azure::storage::cloud_blob_container container = m_BlobClient.get_container_reference(utility::conversions::to_string_t(folder_name));
			azure::storage::list_blob_item_segment blobs = container.list_blobs_segmented(blobs.continuation_token);
			int contentsSize = 0;
			for each(azure::storage::list_blob_item blob in blobs.results())
			{
				utility::string_t blobActual(((azure::storage::cloud_block_blob)blob.as_blob()).name());
				for (int i = 0; i < blobActual.size(); ++i)
				{
					contentsSize++;
				}
				contentsSize++;
			}
			files = new char[contentsSize];
			int i = 0;
			for each(azure::storage::list_blob_item blob in blobs.results())
			{
				utility::string_t blobActual(((azure::storage::cloud_block_blob)blob.as_blob()).name());
				for (int j = 0; j < blobActual.size(); ++j)
				{
					files[i] = blobActual[j];
					i++;
				}
				files[i] = '#';
				i++;
			}
		}
		catch (const std::exception e)
		{
			std::wcout << U("Error: ") << e.what() << std::endl;
			files = new char[0];
			return files;
		}
	}

	char* FileRetrieve(char* folder_name, char* file_name)
	{
		char* file;
		try
		{
			azure::storage::cloud_blob_container container = m_BlobClient.get_container_reference(utility::conversions::to_string_t(folder_name));
			azure::storage::cloud_block_blob blob = container.get_block_blob_reference(utility::conversions::to_string_t(file_name));

			utility::string_t blobActual = blob.download_text();
			file = new char[blobActual.size()];
			file[blobActual.size()] = '\0';
			for (int i = 0; i < blobActual.size(); ++i)
			{
				file[i] = blobActual[i];
			}
			return file;
		}
		catch (const std::exception e)
		{
			std::wcout << U("Error: ") << e.what() << std::endl;
			file = new char[0];
			return file;
		}
	}
}