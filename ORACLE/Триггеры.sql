create or replace TRIGGER AFTER_RESOLVE_REQUEST 
AFTER INSERT ON REQUEST
DECLARE
  total NUMBER;
  max_quantity NUMBER;
  book_available NUMBER;
  book_requested NUMBER;
  requestID NUMBER;
  cardID NUMBER;
  bookID NUMBER;
  return_date DATE;
BEGIN
  -- ������������ ���������� ����� ���� �� ����� � ��������
  SELECT GET_DEFAULT_MAX_BOOK_QUANTITY() INTO max_quantity FROM DUAL;
  -- ���� �������� �� ���������
  SELECT get_default_return_date() INTO return_date FROM DUAL;
  
  FOR I IN 1 .. request_state.new_rows.count LOOP
    requestID := request_state.new_rows(i).RequestID;
    cardID := request_state.new_rows(i).CardID;
    bookID := request_state.new_rows(i).BookID;
    book_requested := request_state.new_rows(i).BookQuantity;
    
    -- ����� ���� �� ����� � ������������
    SELECT get_unreturned_book_quantity(cardID) INTO total FROM dual;
    -- ����� ����������� ����� � ����������
    SELECT Book_Quantity INTO book_available FROM Book WHERE Book_ID = bookID;
    
    IF book_available < book_requested THEN
    ---���� ���� ������������, �� ����� �� ������� ��������
    INSERT INTO Request_Rejected (Request_Rejected_Request_ID, Request_Rejected_Book_ID, Request_Rejected_Reason_ID)
    VALUES (requestID, bookID, 1);
    DBMS_OUTPUT.put_line('����� �� ������� ��������');
  ELSIF total + book_requested > max_quantity THEN
    -- ���� �� ����� ������� �����, ����� �� ������� �������� ���������� ����
    INSERT INTO Request_Rejected (Request_Rejected_Request_ID, Request_Rejected_Book_ID, Request_Rejected_Reason_ID)
    VALUES (requestID, bookID, 2);
    DBMS_OUTPUT.put_line('��������� ���� ������ ��� ��������� ����� �� �����');
  ELSE
      -- ��������� �������� ����� ���� � ������� ����
      UPDATE Book SET Book_Quantity = book_available - book_requested WHERE Book_ID = bookID;
      -- ��� ���������, ������ �����
      INSERT INTO Request_Approved (Request_Approved_Request_ID, Request_Approved_Book_ID, Request_Approved_Return_Date)
      VALUES (requestID, bookID, return_date);
      DBMS_OUTPUT.put_line('����� ������');
  END IF;
  END LOOP;
END;

create or replace TRIGGER BEFORE_RESOLVE_REQUEST 
BEFORE INSERT ON REQUEST 
BEGIN
  request_state.new_rows := request_state.empty;
END;

create or replace TRIGGER RESOLVE_REQUEST 
AFTER INSERT ON REQUEST FOR EACH ROW
DECLARE
  item request_state.CardBook;

BEGIN
    item.RequestID := :new.Request_ID;
    item.CardID :=:new.Request_Card_ID;
    item.BookID := :new.Request_Book_ID;
    item.BookQuantity := :new.Request_Book_Quantity;
    request_state.new_rows(request_state.new_rows.count + 1) := item;
END;

CREATE OR REPLACE TRIGGER PUBLISHER_GENERATE_ID 
BEFORE INSERT ON PUBLISHER 
FOR EACH ROW
BEGIN
  SELECT Publisher_Sequence.NEXTVAL INTO :new.Publisher_ID FROM dual;
END;

CREATE OR REPLACE TRIGGER RUBRIC_GENERATE_ID 
BEFORE INSERT ON RUBRIC 
FOR EACH ROW
BEGIN
  SELECT Rubric_Sequence.NEXTVAL INTO :new.Rubric_ID FROM dual;
END;