CREATE PROCEDURE [dbo].[spCalculation_GetById]
	@Id int
AS
begin

	select * from dbo.[Calculations]
	where [Id] = @Id;

end
