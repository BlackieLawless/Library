--������� ������	
INSERT INTO Reject_Reason (Reject_Reason_ID, Reject_Reason_Name)
  VALUES(1, '����������� �� ������');
INSERT INTO Reject_Reason (Reject_Reason_ID, Reject_Reason_Name)
  VALUES(2, '�������� ���������� ����� ����');
INSERT INTO Reject_Reason (Reject_Reason_ID, Reject_Reason_Name)
  VALUES(3, '������ ����������� �� ������� ��������� ����� ��������');
  
--���� �����������
INSERT INTO Notification_Type (Notification_Type_ID, Notification_Text)
	VALUES(1, '����� ������');
INSERT INTO Notification_Type (Notification_Type_ID, Notification_Text)
	VALUES(2, '������ ����������� �� ��������� ����� ��������');
	
--���� �������������
INSERT INTO Role (Role_ID, Role_Name)
	VALUES(7, '�������������');
INSERT INTO Role (Role_ID, Role_Name)
	VALUES(1, '����������');
INSERT INTO Role (Role_ID, Role_Name)
	VALUES(2, '��������');
INSERT INTO Role (Role_ID, Role_Name)
	VALUES(4, '���������� �����������');

--������������
INSERT INTO Employee (Employee_ID, Employee_Name, Employee_Password, Employee_Role_ID)
  VALUES(0, '�������������', '1917EE334D5A87B30E538265BF484448C46E70B69ADA3D599A64D8EF6E01F7F9', 7); 
INSERT INTO Employee (Employee_ID, Employee_Name, Employee_Password, Employee_Role_ID)
  VALUES(1, '����������', 'E7CA2C35359D856D014AEE9AACB65E56BFC8A295272539E3632E476C8271456A', 1);
INSERT INTO Employee (Employee_ID, Employee_Name, Employee_Password, Employee_Role_ID)
  VALUES(2, '��������', '85516918546A9BFF3CAADDD2BED0B2B36CC740D341A863C8BE40F822B19CC487', 2);
INSERT INTO Employee (Employee_ID, Employee_Name, Employee_Password, Employee_Role_ID)
  VALUES(3, '���������� �����������', '5D9251E95E1746D07CDE7152E1F3B008F58D3E7F98C609DD6214DCF4441DB75C', 4);